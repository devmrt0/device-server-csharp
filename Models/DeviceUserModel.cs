using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Linq;
using Newtonsoft.Json;



namespace device_server_c_
{
    public class DeviceUserModel
    {
        public List<Item> DeviceList = new List<Item>();

        public class Item
        {
            public string id { get; set; }
            public string name { get; set; }
            public string password { get; set; }

        }


        public void Load()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\devices.json");
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {

                    DeviceList.Add(new Item() { id = item.id, name = item.name, password = item.password });

                }


            }
        }

        public void saveDeviceFile()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\devices.json");
            var json = JsonConvert.SerializeObject(DeviceList);
            File.WriteAllText(file, json);

        }

        public void saveDevice(string id, Item body)
        {
            Load();
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                if (body.name != null || body.name != "")
                {
                    DeviceList[index].name = body.name;
                }
                if (body.password != null || body.password != "")
                {
                    DeviceList[index].password = body.password;
                }
                saveDeviceFile();
            }
            else
            {
                DeviceList.Add(new Item { id = body.id, name = body.name, password = body.password });
                saveDeviceFile();
            }

        }

        public void deleteDevice(string id)
        {
            Load();
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                DeviceList.RemoveAt(index);
                saveDeviceFile();
            }
        }

        public Item authDevice(string id, string password)
        {
            Load();
            var index = DeviceList.FindIndex(a => a.id == id);
            var pass = "";
            if (index != -1)
            {
                pass = DeviceList[index].password;
            }
            if (password == pass)
            {
                return new Item { id = DeviceList[index].id, name = DeviceList[index].name, password = DeviceList[index].password };
            }
            else
            {
                return new Item { id = "", name = "", password = "" };
            }

        }

        public string get()
        {
            var json = JsonConvert.SerializeObject(DeviceList);
            if (DeviceList.Count == 0)
            {
                return ApiResponse.Warning(10007, "");
            }
            else
            {
                return ApiResponse.Success(json, "");
            }

        }

        public object get2()
        {
            Load();
            var json = DeviceList;
            if (DeviceList.Count == 0)
            {
                return ApiResponse.Warning2(10007, "");
            }
            else
            {
                return ApiResponse.Success2(json, "");
            }

        }

        public string getById(string id)
        {
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                return ApiResponse.Success(DeviceList[index], "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }

        public object getByIdDevice2(string id)
        {
            Load();
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                return ApiResponse.Success2(DeviceList[index], "");
            }
            else
            {
                return ApiResponse.Warning2(10007, "");
            }

        }

        public Item getDeviceId(string id)
        {
            Load();
            var index = DeviceList.FindIndex(a => a.id == id);

            return new Item
            {
                id = DeviceList[index].id,
                name = DeviceList[index].name,
                password = DeviceList[index].password,
                

            };

        }
        public string Post(Item body)
        {
            saveDevice(body.id, body);
            return ApiResponse.Success("", "");
        }

        public object Post2(Item body)
        {
            saveDevice(body.id, body);
            return ApiResponse.Success2("", "");
        }


        public string PutById(string id, Item body)
        {
            saveDevice(id, body);
            return ApiResponse.Success("", "");
        }

        public object PutById2(string id, Item body)
        {
            saveDevice(id, body);
            return ApiResponse.Success2("", "");
        }


        public string DeleteById(string id)
        {
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                deleteDevice(id);
                return ApiResponse.Success("", "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }


        public object DeleteById2(string id)
        {
            var index = DeviceList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                deleteDevice(id);
                return ApiResponse.Success2("", "");
            }
            else
            {
                return ApiResponse.Warning2(10007, "");
            }

        }






    }
}