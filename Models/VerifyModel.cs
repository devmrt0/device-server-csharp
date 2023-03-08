using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Mustache;
using Mustachio;
using Xunit;
using Xunit.Extensions;







namespace device_server_c_
{
    public class UsersModel
    {
        public List<Item> UserList = new List<Item>();

        public class Item
        {
            public string uid { get; set; }
            public string name { get; set; }
            public string position { get; set; }
            public string photo { get; set; }
            public bool access_enabled { get; set; }

        }








        public void Load()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\whitelist.json");
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    UserList.Add(new Item { uid = item.uid, name = item.name, position = item.position, photo = item.photo, access_enabled = item.access_enabled });
                }


            }
        }

        public void saveUserFile()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\whitelist.json");
            var json = JsonConvert.SerializeObject(UserList);
            File.WriteAllText(file, json);

        }

        public object prepareScreen(int id, string view, string photo)
        {
            ScreenModel myScreen = new ScreenModel();
            var screen = myScreen.getScreenId(id);
            var model = new Dictionary<string, object>();
            var template = Mustachio.Parser.Parse(view)(model);
            model[screen.line_1.text] = screen.line_1.text;
            model[screen.line_2.text] = screen.line_2.text;
            model[screen.line_3.text] = screen.line_3.text;
            model[screen.footer.text] = screen.footer.text;
            model[photo] = photo;
            Assert.Equal(screen.line_1.text, template);
            Assert.Equal(screen.line_2.text, template);
            Assert.Equal(screen.line_3.text, template);
            Assert.Equal(screen.footer.text, template);
            Assert.Equal(photo, template);
            return model;

        }

        public object verifyUid(string id, string deviceid, string language)
        {
            Load();
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                var user = UserList[index];
                if (user.access_enabled == true)
                {
                    var response = new { name = user.name, position = user.position, deviceid = deviceid, uid = id };
                    return ApiResponse.Success2(prepareScreen(1, JsonConvert.SerializeObject(response), user.photo), "");
                }
                else
                {
                    var response = new { name = user.name, position = user.position, deviceid = deviceid, uid = id };
                    return ApiResponse.Success2(prepareScreen(6, JsonConvert.SerializeObject(response), user.photo), "");
                }

            }
            else
            {
                var response = new { deviceid = deviceid, uid = id };
                return ApiResponse.Success(prepareScreen(2, JsonConvert.SerializeObject(response), null), "");
            }
        }

        public string verifyFace()
        {
            return "sonra geliştirilecek";
        }

        public string verifyQR()
        {
            return "sonra geliştirilecek";
        }

        public void saveUser(string id, Item body)
        {
            Load();
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                if (body.name != null && body.name != "")
                {
                    UserList[index].name = body.name;
                }
                if (body.position != null && body.position != "")
                {
                    UserList[index].position = body.position;
                }
                if (body.photo != null && body.photo != "")
                {
                    UserList[index].photo = body.photo;
                }
                if (body.access_enabled == true || body.access_enabled == false)
                {
                    UserList[index].access_enabled = body.access_enabled;
                }
                saveUserFile();
            }
            else
            {
                UserList.Add(new Item { uid = body.uid, name = body.name, position = body.position, photo = body.photo, access_enabled = body.access_enabled });
                saveUserFile();
            }

        }

        public void deleteUser(string id)
        {
            Load();
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                UserList.RemoveAt(index);
                saveUserFile();
            }
        }

        public Item authUser(string id, string password)
        {
            var index = UserList.FindIndex(a => a.uid == id);
            var pass = UserList[index].position;
            if (password == pass)
            {
                return new Item { uid = UserList[index].uid, name = UserList[index].name, position = UserList[index].position };
            }
            else
            {
                return new Item { uid = "", name = "", position = "" };
            }

        }

        public string getUserAll()
        {
            var json = JsonConvert.SerializeObject(UserList);
            if (UserList.Count == 0)
            {
                return ApiResponse.Warning(10007, "");
            }
            else
            {
                return ApiResponse.Success(json, "");
            }

        }

        public object getUserAll2()
        {
            Load();
            var json = UserList;
            if (UserList.Count == 0)
            {
                return ApiResponse.Warning2(10007, "");
            }
            else
            {
                return ApiResponse.Success2(json, "");
            }

        }

        public string getByIdUser(string id)
        {
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                return ApiResponse.Success(UserList[index], "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }

        public object getByIdUser2(string id)
        {
            Load();
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                return ApiResponse.Success2(UserList[index], "");
            }
            else
            {
                return ApiResponse.Warning2(10007, "");
            }

        }

        public Item getUserId(string id)
        {
            Load();
            var index = UserList.FindIndex(a => a.uid == id);

            return new Item
            {
                uid = UserList[index].uid,
                name = UserList[index].name,
                position = UserList[index].position,
                photo = UserList[index].photo,
                access_enabled = UserList[index].access_enabled,

            };

        }
        public string PostUser(Item body)
        {
            saveUser(body.uid, body);
            return ApiResponse.Success("", "");
        }

        public object PostUser2(Item body)
        {
            saveUser(body.uid, body);
            return ApiResponse.Success2("", "");
        }


        public string PutUserById(string id, Item body)
        {
            saveUser(id, body);
            return ApiResponse.Success("", "");
        }

        public object PutUserById2(string id, Item body)
        {
            saveUser(id, body);
            return ApiResponse.Success2("", "");
        }


        public string DeleteUserById(string id)
        {
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                deleteUser(id);
                return ApiResponse.Success("", "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }


        public object DeleteUserById2(string id)
        {
            var index = UserList.FindIndex(a => a.uid == id);
            if (index != -1)
            {
                deleteUser(id);
                return ApiResponse.Success2("", "");
            }
            else
            {
                return ApiResponse.Warning2(10007, "");
            }

        }





    }
}
