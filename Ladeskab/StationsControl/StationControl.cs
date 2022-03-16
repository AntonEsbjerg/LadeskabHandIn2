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
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        public LadeskabState _state;
        private uint _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IChargeControl _chargeControl;
        public bool CurrentDoorStatus;
        private IRfidReader _reader;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IChargeControl chargeControl, IRfidReader reader, IDisplay display)
        {
            _door = door;
            _chargeControl = chargeControl;
            _reader = reader;
            _display = display;

            door.DoorEvent += HandleDoorChangedEvent;
            reader.RfidEvent += HandleRfidChangedEvent;
        }
        
        public void DoorOpened()
        {
            _display.Print("Tilslut telefon");
        }

        public void DoorClosed()
        {
            _display.Print("Indlæs RFID");
        }

        public void RFIDDetected(uint id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
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
                    if (CheckId(_oldId, id))
                    {
                        _chargeControl.StopCharge();
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

        private void HandleDoorChangedEvent(object sender, DoorEventArgs e)
        {
            CurrentDoorStatus = e.IsOpen;
        }

        private void HandleRfidChangedEvent(object sender, RfidEventArgs e)
        {
            RFIDDetected(e.Rfid);
        }

        public bool CheckId(uint oldId, uint Id)
        {
            if (Id == oldId)
                return true;

            return false;
        }
    }
}
