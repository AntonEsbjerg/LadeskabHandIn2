using System;
using System.Collections.Generic;
using System.Globalization;
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
            string fileTime = time.ToString(CultureInfo.CurrentCulture);
            string[] splitStrings;
            if (path.Contains("JenkinsData"))
            {
                splitStrings = path.Split(@"\LadeSkab01F22");
            }
            else
            {
                splitStrings = path.Split(@"\LadeskabHandIn2");
            }
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                 fileTime= fileTime.Replace(c.ToString(), "");
            }
            File.WriteAllText(splitStrings[0] + @"\LadeSkabHandIn2\Ladeskab\Logfolder\" + fileTime +" Locked", json);
            var readFile = File.ReadAllText(splitStrings[0] + @"\LadeskabHandIn2\Ladeskab\Logfolder\" + fileTime+ " Locked");
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
            string fileTime = time.ToLongTimeString();
            string[] splitStrings;
            if (path.Contains("JenkinsData"))
            {
                splitStrings = path.Split(@"\LadeSkab01F22");
            }
            else
            {
                splitStrings = path.Split(@"\LadeskabHandIn2");
            }
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                fileTime = fileTime.Replace(c.ToString(), "");
            }
            File.WriteAllText(splitStrings[0] + @"\LadeskabHandIn2\Ladeskab\Logfolder\" + fileTime+ " Unlocked", json);
            var readFile = File.ReadAllText(splitStrings[0] + @"\LadeskabHandIn2\Ladeskab\Logfolder\" + fileTime + " Unlocked");
            IJsonFileModel model = JsonSerializer.Deserialize<JsonFileModel>(readFile);
            return model;
        }
    }
}