using System;

namespace Ladeskab
{
    public interface IJsonLogger
    {
        IJsonFileModel LogDoorLocked(uint rfid, DateTime time);
        IJsonFileModel LogDoorUnlocked(uint rfid, DateTime time);
    }
}