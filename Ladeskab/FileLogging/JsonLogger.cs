using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ladeskab
{
    public class JsonLogger: IJsonLogger
    {
        public IJsonFileModel LogDoorLocked(uint rfid, DateTime time)
        {
            IJsonFileModel jsonInput=
            new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked= true
            };
            string json = JsonSerializer.Serialize(jsonInput);
            var path = Environment.CurrentDirectory;
            string[] splitStrings = path.Split(@"\LadeskabHandIn2");
            File.WriteAllText(splitStrings[0] + @"\LadeSkabHandIn2\Ladeskab\Log", json);
            var readFile = File.ReadAllText(splitStrings[0] + @"\LadeSkabHandIn2\Ladeskab\Log");
            IJsonFileModel model = JsonSerializer.Deserialize<JsonFileModel>(readFile);
            return model;
        }
        public IJsonFileModel LogDoorUnlocked(uint rfid, DateTime time)
        {
            IJsonFileModel jsonInput =
                new JsonFileModel()
                {
                    Rfid = rfid,
                    Time = time,
                    IsDoorLocked = false
                };
            string json = JsonSerializer.Serialize(jsonInput);
            var path = Environment.CurrentDirectory;
            string[] splitStrings = path.Split(@"\LadeskabHandIn2");
            File.WriteAllText(splitStrings[0] + @"\LadeSkabHandIn2\Ladeskab\Log", json);
            var readFile = File.ReadAllText(splitStrings[0] + @"\LadeSkabHandIn2\Ladeskab\Log");
            IJsonFileModel model = JsonSerializer.Deserialize<JsonFileModel>(readFile);
            return model;
        }
    }
}