using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace device_server_c_
{
    public class ScreenModel
    {
        public List<Item> ScreenList = new List<Item>();

        public class Item
        {
            public int id { get; set; }
            public int screen_type { get; set; }
            public int default_screen_type { get; set; }
            public string icon_id { get; set; }

            public string audio_id { get; set; }

            public int screen_duration { get; set; }

            public int tr_out_1_duration { get; set; }

            public int tr_out_2_duration { get; set; }

            public int rl_1_duration { get; set; }

            public int rl_2_duration { get; set; }

            public int sound_duration { get; set; }

            public int next_id { get; set; }

            public Line1 line_1 { get; set; }

            public Line2 line_2 { get; set; }

            public Line3 line_3 { get; set; }

            public Footer footer { get; set; }

            public bool infinite { get; set; }

            public bool is_blink { get; set; }
        }

        public class Line1
        {
            public string text { get; set; }
            public string font_name { get; set; }
            public string font_color { get; set; }
            public string background_color { get; set; }

        }

        public class Line2
        {
            public string text { get; set; }
            public string font_name { get; set; }
            public string font_color { get; set; }
            public string background_color { get; set; }

        }

        public class Line3
        {
            public string text { get; set; }
            public string font_name { get; set; }
            public string font_color { get; set; }
            public string background_color { get; set; }

        }

        public class Footer
        {
            public string text { get; set; }
            public string font_name { get; set; }
            public string font_color { get; set; }
            public string background_color { get; set; }

        }


        public void Load()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\screen.json");
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {



                    ScreenList.Add(new Item
                    {
                        id = item.id,
                        screen_type = item.screen_type,
                        default_screen_type = item.default_screen_type,
                        screen_duration = item.screen_duration,
                        sound_duration = item.sound_duration,
                        tr_out_1_duration = item.tr_out_1_duration,
                        tr_out_2_duration = item.tr_out_2_duration,
                        rl_1_duration = item.rl_1_duration,
                        rl_2_duration = item.rl_2_duration,
                        infinite = item.infinite,
                        next_id = item.next_id,
                        is_blink = item.is_blink,
                        icon_id = item.icon_id,
                        line_1 = new Line1 { text = item.line_1.text, font_name = item.line_1.font_name, font_color = item.line_1.font_color, background_color = item.line_1.background_color },
                        line_2 = new Line2 { text = item.line_2.text, font_name = item.line_2.font_name, font_color = item.line_2.font_color, background_color = item.line_2.background_color },
                        line_3 = new Line3 { text = item.line_3.text, font_name = item.line_3.font_name, font_color = item.line_3.font_color, background_color = item.line_3.background_color },
                        footer = new Footer { text = item.footer.text, font_name = item.footer.font_name, font_color = item.footer.font_color, background_color = item.footer.background_color },
                        audio_id = item.audio_id
                    });
                }


            }
        }

        public void saveScreenFile()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"Desktop\device-server-c#\Datas\screen.json");
            var json = JsonConvert.SerializeObject(ScreenList);
            File.WriteAllText(file, json);

        }

        public void saveScreen(int id, Item body)
        {
            Load();
            var index = ScreenList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                if (body.screen_type != 0)
                {
                    ScreenList[index].screen_type = body.screen_type;
                }
                if (body.default_screen_type != 0)
                {
                    ScreenList[index].default_screen_type = body.default_screen_type;
                }
                if (body.screen_duration != 0)
                {
                    ScreenList[index].screen_duration = body.screen_duration;
                }
                if (body.sound_duration != 0)
                {
                    ScreenList[index].sound_duration = body.sound_duration;
                }
                if (body.audio_id != null && body.audio_id != "")
                {
                    ScreenList[index].audio_id = body.audio_id;
                }
                if (body.icon_id != null && body.icon_id != "")
                {
                    ScreenList[index].icon_id = body.icon_id;
                }
                if (body.next_id != 0)
                {
                    ScreenList[index].next_id = body.next_id;
                }
                if (body.tr_out_1_duration != 0)
                {
                    ScreenList[index].tr_out_1_duration = body.tr_out_1_duration;
                }
                if (body.tr_out_2_duration != 0)
                {
                    ScreenList[index].tr_out_2_duration = body.tr_out_2_duration;
                }
                if (body.rl_2_duration != 0)
                {
                    ScreenList[index].rl_2_duration = body.rl_2_duration;
                }
                if (body.rl_1_duration != 0)
                {
                    ScreenList[index].rl_1_duration = body.rl_1_duration;
                }
                if (body.is_blink == true || body.is_blink == false)
                {
                    ScreenList[index].is_blink = body.is_blink;
                }
                if (body.infinite == true || body.infinite == false)
                {
                    ScreenList[index].infinite = body.infinite;
                }
                if (body.line_1 != null)
                {
                    ScreenList[index].line_1 = body.line_1;
                }
                if (body.line_2 != null)
                {
                    ScreenList[index].line_2 = body.line_2;
                }
                if (body.line_3 != null)
                {
                    ScreenList[index].line_3 = body.line_3;
                }
                if (body.footer != null)
                {
                    ScreenList[index].footer = body.footer;
                }


                saveScreenFile();
            }
            else
            {
                ScreenList.Add(new Item
                {
                    id = body.id,
                    screen_type = body.screen_type,
                    default_screen_type = body.default_screen_type,
                    screen_duration = body.screen_duration,
                    sound_duration = body.sound_duration,
                    tr_out_1_duration = body.tr_out_1_duration,
                    tr_out_2_duration = body.tr_out_2_duration,
                    rl_1_duration = body.rl_1_duration,
                    rl_2_duration = body.rl_2_duration,
                    infinite = body.infinite,
                    next_id = body.next_id,
                    is_blink = body.is_blink,
                    icon_id = body.icon_id,
                    line_1 = body.line_1,
                    line_2 = body.line_2,
                    line_3 = body.line_3,
                    footer = body.footer,
                    audio_id = body.audio_id
                });
                saveScreenFile();
            }

        }

        public void deleteScreen(int id)
        {
            Load();
            var index = ScreenList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                ScreenList.RemoveAt(index);
                saveScreenFile();
            }
        }

        public Item authScreen(int id, string audio_id)
        {
            var index = ScreenList.FindIndex(a => a.id == id);
            var pass = ScreenList[index].audio_id;
            if (audio_id == pass)
            {
                return new Item
                {
                    id = ScreenList[index].id,
                    screen_type = ScreenList[index].screen_type,
                    audio_id = ScreenList[index].audio_id

                };
            }
            else
            {
                return new Item { id = 0, screen_type = 0, audio_id = "" };
            }

        }

        public string getScreen()
        {
            var json = JsonConvert.SerializeObject(ScreenList);
            if (ScreenList.Count == 0)
            {
                return ApiResponse.Warning(10007, "");
            }
            else
            {
                return ApiResponse.Success(json, "");
            }

        }

        public object getScreen2()
        {
            Load();
            var json = ScreenList;
            if (ScreenList.Count == 0)
            {
                return ApiResponse.Warning2(10007, "");
            }
            else
            {
                return ApiResponse.Success2(json, "");
            }

        }

        public Item getScreenId(int id)
        {
            Load();
            var index = ScreenList.FindIndex(a => a.id == id);

            return new Item
            {
                id = ScreenList[index].id,
                screen_type = ScreenList[index].screen_type,
                default_screen_type = ScreenList[index].default_screen_type,
                screen_duration = ScreenList[index].screen_duration,
                sound_duration = ScreenList[index].sound_duration,
                tr_out_1_duration = ScreenList[index].tr_out_1_duration,
                tr_out_2_duration = ScreenList[index].tr_out_2_duration,
                rl_1_duration = ScreenList[index].rl_1_duration,
                rl_2_duration = ScreenList[index].rl_2_duration,
                infinite = ScreenList[index].infinite,
                next_id = ScreenList[index].next_id,
                is_blink = ScreenList[index].is_blink,
                icon_id = ScreenList[index].icon_id,
                line_1 = ScreenList[index].line_1,
                line_2 = ScreenList[index].line_2,
                line_3 = ScreenList[index].line_3,
                footer = ScreenList[index].footer,
                audio_id = ScreenList[index].audio_id
            };

        }



        public string getScreenById(int id)
        {
            var index = ScreenList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                return ApiResponse.Success(ScreenList[index], "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }
        public string PostScreen(Item body)
        {
            saveScreen(body.id, body);
            return ApiResponse.Success("", "");
        }

        public object PostScreen2(Item body)
        {
            saveScreen(body.id, body);
            return ApiResponse.Success2("", "");
        }


        public string PutScreenById(int id, Item body)
        {
            saveScreen(id, body);
            return ApiResponse.Success("", "");
        }

        public object PutScreenById2(int id, Item body)
        {
            saveScreen(id, body);
            return ApiResponse.Success2("", "");
        }


        public string DeleteScreenById(int id)
        {
            var index = ScreenList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                deleteScreen(id);
                return ApiResponse.Success("", "");
            }
            else
            {
                return ApiResponse.Warning(10007, "");
            }

        }


        public object DeleteScreenById2(int id)
        {
            var index = ScreenList.FindIndex(a => a.id == id);
            if (index != -1)
            {
                deleteScreen(id);
                return ApiResponse.Success2("", "");
            }
            else
            {
                return ApiResponse.Warning2(10007, "");
            }

        }






    }
}