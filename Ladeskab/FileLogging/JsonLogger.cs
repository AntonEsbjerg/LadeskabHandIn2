using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ladeskab
{
    public class JsonLogger: IJsonLogger
    {
        public string LogDoorLocked(uint rfid, DateTime time)
        {
            List<IJsonFileModel> jsonList = new List<IJsonFileModel>();
            jsonList.Add(new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked= true
            });
            string json = JsonSerializer.Serialize(jsonList);
            return json;
        }
        public string LogDoorUnlocked(uint rfid, DateTime time)
        {
            List<IJsonFileModel> jsonList = new List<IJsonFileModel>();
            jsonList.Add(new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked = false
            });
            string json = JsonSerializer.Serialize(jsonList);
            return json;
        }
    }
}