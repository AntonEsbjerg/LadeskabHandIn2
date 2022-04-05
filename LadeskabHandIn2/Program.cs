using System;
using System.IO;
using Ladeskab;
using Ladeskab.RfidReaders;
using Ladeskab.Doors;

namespace LadeskabHandIn2
{
    class Program
    {
        private static IDoor _door;
        private static IStationControl _control;
        private static IUsbCharger _usbCharger;
        private static IChargeControl _chargeControl;
        private static IRfidReader _rfidReader;
        private static IDisplay _display;
        private static IJsonLogger _jsonLogger;
        private static StringWriter _sw;
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            _door = new Door();
            _sw = new StringWriter();
            _usbCharger = new UsbChargerSimulator();
            _rfidReader = new RfidReader();
            _display = new Display(_sw);
            _chargeControl = new ChargeControl(_usbCharger,_display);
            _jsonLogger = new JsonLogger();

            _control = new StationControl(_door, _chargeControl, _rfidReader,_display, _jsonLogger);
            bool finish = false;

            _display.Print("\nMulige indtastninger");
            _display.Print("E = Exit\nO = Open\nC = Close\nR=RFID\nP=Plugin\nU=Unplug");

            do
            {
                string input;
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        _door.OnDoorOpen();
                        break;

                    case 'C':
                        _door.OnDoorClose();
                        break;

                    case 'R':
                        Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        uint id = Convert.ToUInt16(idString);

                        _rfidReader.ReadRfid(id,DateTime.Now);
                        break;

                    case 'P':
                        _chargeControl.PluginPhone();
                        break;

                    case 'U':
                        _chargeControl.UnplugPhone();
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
