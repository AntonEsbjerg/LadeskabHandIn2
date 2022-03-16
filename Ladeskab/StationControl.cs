using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using Ladeskab.Doors;
using Ladeskab.EventArgs;
using Ladeskab.RfidReaders;

namespace Ladeskab
{
    public class StationControl:IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private uint _oldId;
        private IDoor _door;
        public bool CurrentDoorStatus;
        //new
        public double _current;
        private IRfidReader _reader;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil



        // Her mangler constructor
        public StationControl(IDoor door, IUsbCharger charger, IRfidReader reader)
        {
            door.DoorEvent += HandleDoorChangedEvent;

            //classes needed
            charger.CurrentValueEvent += HandleCurrentChangedEvent;
            reader.RfidEvent += HandleRfidChangedEvent;

        }
        

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen

        // Her mangler de andre trigger handlere
        public void DoorOpened()
        {
            throw new NotImplementedException();
        }

        public void DoorClosed()
        {
            throw new NotImplementedException();
        }

        public void RFIDDetected(uint id)
        {
            switch (_state)
            {
                case LadeskabState.Available:

                    // Check for ladeforbindelse



                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _reader.ReadRfid(id);

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. " +
                                          "Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. " +
                                          "Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    Console.WriteLine("The door is open...");
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleDoorChangedEvent(object sender, DoorEventArgs e)
        {
            CurrentDoorStatus = e.IsOpen;
        }


        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            _current = e.Current;
        }

        private void HandleRfidChangedEvent(object sender, RfidEventArgs e)
        {
            _oldId = e.Rfid;
        }


        public bool CheckId(int oldId, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
