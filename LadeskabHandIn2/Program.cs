using System;
using Ladeskab;
using Ladeskab.RfidReaders;

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
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            _door = new Door();
            _usbCharger = new UsbChargerSimulator();
            _chargeControl = new ChargeControl(_usbCharger);
            _rfidReader = new RfidReader();
            _display = new Display();

            _control = new StationControl(_door, _chargeControl, _rfidReader,_display);
            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
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

                        _rfidReader.ReadRfid(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
