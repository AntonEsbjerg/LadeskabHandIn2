using System;
using Ladeskab;

namespace LadeskabHandIn2
{
    class Program
    {
        private static Door _door;
        private static StationControl control;
        static void Main(string[] args)
        {
				// Assemble your system here from all the classes
            
            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

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

                        control.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
