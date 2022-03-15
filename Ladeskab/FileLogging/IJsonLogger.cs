using System;

namespace Ladeskab
{
    public interface IJsonLogger
    {
        string LogDoorLocked(uint rfid, DateTime time);
        string LogDoorUnlocked(uint rfid, DateTime time);
    }
}