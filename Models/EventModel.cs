using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;



namespace device_server_c_
{


    public class EventModel
    {
        public List<Item> EventList = new List<Item>();

        public class Item
        {
            public string device_id { get; set; }
            public string event_type { get; set; }
            public string event_dt { get; set; }



        }

        public string doorOpen(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openDoor");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success("", "");
        }

        public object doorOpen2(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openDoor");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success2("", "");
        }

        public string doorAlarmOpen(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openAlarmDoor");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success("", "");
        }

        public object doorAlarmOpen2(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openAlarmDoor");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success2("", "");
        }


        public string doorAlarmClose(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openAlarmClose");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success("", "");
        }

        public object doorAlarmClose2(string device_id, string event_type, string event_dt)
        {
            var deg = System.Guid.NewGuid().ToString("openAlarmClose");
            EventList.Add(new Item { device_id = device_id, event_type = deg, event_dt = event_dt });
            return ApiResponse.Success2("", "");
        }

    }
}