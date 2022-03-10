using System;

namespace Ladeskab
{
    public class JsonFileModel: IJsonFileModel
    {
        public uint Rfid { get; set; }
        public DateTime Time { get; set; }
        public bool IsDoorLocked { get; set; }
    }
}