using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp;
using WebSocketSharp.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Dynamic;





namespace device_server_c_
{
    public class Commands
    {
        public List<Item> CommandList = new List<Item>();

        public class Item
        {
            public string url { get; set; }
            public string http_method { get; set; }
            public string method_type { get; set; }
            public string method_name { get; set; }

        }


        public void Load()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\commands.json");
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    CommandList.Add(new Item { url = item.url, http_method = item.http_method, method_type = item.method_type, method_name = item.method_name });
                }


            }
        }

        public void saveCommandFile()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Datas\commands.json");
            var json = JsonConvert.SerializeObject(CommandList);
            File.WriteAllText(file, json);

        }

        public object getCommand(string url, string http_method, string method_type)
        {
            Load();
            var index = CommandList.FindIndex(a => (a.url == url) && (a.http_method == http_method) && (a.method_type == method_type));
            return CommandList[index];

        }


        public object getCommandEx(string url, string http_method, string id, bool isArray)
        {
            if ((http_method == "post") && (id == null))
            {
                if (isArray == true)
                {
                    return getCommand(url, http_method, "all");
                }
                else
                {
                    return getCommand(url, http_method, "id");
                }

            }
            else
            {
                if (id == null)
                {
                    return getCommand(url, http_method, "all");
                }
                else
                {
                    return getCommand(url, http_method, "id");
                }
            }


        }








    }

    public class Device
    {
        public string id { get; set; }

        public string language { get; set; }

        public string token { get; set; }

        public object ws { get; set; }

        public Dictionary<string, Command> CommandsList = new Dictionary<string, Command>();





        public class Command
        {
            public int status { get; set; }

            public int code { get; set; }

            public string detail { get; set; }

            public string method { get; set; }

            public object data { get; set; }

            public string requestid { get; set; }

        }

        public string sendCommand(string method, object data)
        {   // websocket-sharp
            var wss = new WebSocket("ws://example.com");
            Device mydevice = new Device();
            mydevice.ws = wss;
            Command mycommand = new Command();
            mycommand.status = 0;
            mycommand.code = 0;
            mycommand.detail = "OK";
            mycommand.method = method;
            mycommand.requestid = System.Guid.NewGuid().ToString("B").ToUpper();
            if (data != null || data.ToString() == String.Empty)
            {
                mycommand.data = data;
            }
            CommandsList.Add(mycommand.requestid, mycommand);
            if (data != null || data.ToString() == String.Empty)
            {
                var response = new { method = mycommand.method, data = mycommand.data, requestid = mycommand.requestid };
                wss.Send(JsonConvert.SerializeObject(response));

            }
            else
            {
                var response = new { method = mycommand.method, requestid = mycommand.requestid };
                wss.Send(JsonConvert.SerializeObject(response));
            }

            mycommand.data = null;
            return mycommand.requestid;
        }



        public object addCommand(string method, object data)
        {
            try
            {
                Device mydevice = new Device();
                var requestid = mydevice.sendCommand(method, data);
                var command = CommandsList[requestid];
                var result = ApiResponse.Error2(10011, "", null, "");
                for (int i = 0; i <= 301; i++)
                {
                    if (i % 10 == 0)
                    {
                        if (command.status != 0)
                        {
                            Console.WriteLine("Breaked");
                            break;


                        }

                        else
                        {
                            if (i == 300 && command.status == 0)
                            {
                                break;
                            }
                        }
                    }
                }
                CommandsList.Remove(requestid);
                if (command.status != 0)
                {
                    return command;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception hata)
            {
                var result = new { message = "Server Error:" + hata.Message };
                return result;
            }

        }


        public object parseResponseMessage(Command data)
        {
            try
            {
                if (Common.IsEmptyStr(data.requestid))
                {
                    var result = new { success = false };
                    return result;

                }
                else
                {
                    var result = new { success = true, command = data };
                    return result;

                }

            }

            catch (Exception)
            {
                var result = new { success = false };
                return result;
            }
        }

        public object processCommand(Command data)
        {
            dynamic result = parseResponseMessage(data);
            if (result.success == true)
            {
                var command = CommandsList[result.command.requestid];
                if (command != null)
                {
                    command.status = result.command.status;
                    command.code = result.command.code;
                    command.detail = result.command.detail;
                    if (!Common.IsEmptyStr(result.command.data))
                        command.data = result.command.data;
                    CommandsList[result.command.requestid] = command;
                    return ApiResponse.Success2(null, null);
                }
                else
                {
                    return ApiResponse.Error2(10009, "", null, result.command.requestid);
                }

            }
            else
            {
                return ApiResponse.Error2(10010, "", null, null);
            }


        }

    }


    public class Devices
    {
        public Dictionary<string, Device> DevicesList = new Dictionary<string, Device>();
        public Dictionary<string, Device> TokensList = new Dictionary<string, Device>();


        public bool hasByToken(string token)
        {
            if (TokensList.ContainsKey(token))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool hasByDeviceId(string deviceid)
        {
            if (DevicesList.ContainsKey(deviceid))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public object deleteByDeviceId(string deviceid)
        {
            var device = DevicesList[deviceid];
            if (device != null)
            {
                TokensList.Remove(device.token);
            }

            return DevicesList[deviceid];

        }

        public object deleteByToken(string token)
        {
            var device = TokensList[token];
            if (device != null)
            {
                DevicesList.Remove(device.id);
            }

            return TokensList[token];
        }

        public bool addDevices(string id, string token, string language, object ws)
        {
            Device mydevice = new Device();
            mydevice.id = id;
            mydevice.token = token;
            mydevice.language = language;
            mydevice.ws = ws;
            TokensList.Add(token, mydevice);
            DevicesList.Add(token, mydevice);
            return true;

        }

        public object checkDataIsNull(object data)
        {
            if (data == null)
            {
                return new Object { };
            }
            else
            {
                return data;
            }

        }

        public object AddtoData(object data, string value, string url)
        {
            if (data == null)
            {
                data = new Object { };
            }
            if (url == "black_list")
            {
                data = new { uniqueid = url };
            }
            else
            {
                data = new { id = Int32.Parse(value) };
            }

            return data;
        }

        public object queryStringIdtoData(object data, object query, string url, string id)
        {
            if (query == null)
            {
                if (id != null)
                {
                    var dat = AddtoData(data, url, id);
                    return dat;
                }

            }
            else
            {
                if (data != null)
                {
                    if (id != null)
                    {
                        var dat = AddtoData(data, url, id);
                        return new { data = dat, param = query };


                    }
                }

            }

            return new Object { };
        }


        public object validateSchema(object data, object jsonschema)
        {
            if (jsonschema != null)
            {
                if (data == null)
                {
                    return new { valid = false, error = "body must have a value." };
                }
                else
                {
                    string s = JsonConvert.SerializeObject(jsonschema);
                    string d = JsonConvert.SerializeObject(data);
                    var json_schema = JsonSchema.Parse(s);
                    JObject dat = JObject.Parse(d);
                    bool valid = dat.IsValid(json_schema);
                    return new { valid = valid };

                }
            }

            else
            {
                return new { valid = true, error = "" };
            }

        }

        public string processCommand(string token, Device.Command msg)
        {
            var device = TokensList[token];
            if (device != null)
            {
                Device mydevice = new Device();

                return JsonConvert.SerializeObject(mydevice.processCommand(msg));
            }
            else
            {
                return ApiResponse.Error(10008, "Token Not Found in device list", new { token = token }, "");
            }
        }

        public object addCommandtoList(string http_method, string url, string deviceid, object query, object data, string id)
        {
            Commands myCommands = new Commands();
            var command = myCommands.getCommandEx(url, http_method, id, data.GetType().IsArray);
            if (command != null)
            {
                var device = DevicesList[deviceid];
                if (device != null)
                {
                    var command_data = queryStringIdtoData(data, query, url, id);
                    dynamic vs = validateSchema(command, command_data);
                    dynamic com = command;
                    if (vs.valid)
                    {
                        return device.addCommand(com.method_name, command_data);
                    }
                    else
                    {
                        return ApiResponse.Error2(10013, "", vs.error, "");
                    }

                }
                else
                {
                    return ApiResponse.Error2(10006, "", null, "");
                }

            }
            else
            {
                return ApiResponse.Error2(10012, "", null, "");
            }



        }
    }
}