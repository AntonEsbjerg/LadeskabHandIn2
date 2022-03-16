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
    public class StationControl : IStationControl
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
        private uint _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IChargeControl chargeControl;
        public bool CurrentDoorStatus;
        private IRfidReader _reader;


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil



        // Her mangler constructor
        public StationControl(IDoor door, IChargeControl charger, IRfidReader reader, IDisplay display)
        {
            _door = door;
            chargeControl = charger;
            _reader = reader;

            _display = display;

            door.DoorEvent += HandleDoorChangedEvent;

            //classes needed
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
                    if (chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        chargeControl.StartCharge();
                        _oldId = id;
                        
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.Print("Skabet er låst og din telefon lades. " +
                                          "Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Print("Din telefon er ikke ordentlig tilsluttet. " +
                                          "Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    _display.Print("The door is open...");
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (CheckId(_oldId, id))
                    {
                        chargeControl.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.Print("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Print("Forkert RFID tag"); //RFID Fejl
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleDoorChangedEvent(object sender, DoorEventArgs e)
        {
            CurrentDoorStatus = e.IsOpen;
        }

        private void HandleRfidChangedEvent(object sender, RfidEventArgs e)
        {
            RFIDDetected(e.Rfid);
        }


        public bool CheckId(int oldId, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
