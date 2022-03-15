using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Doors;

namespace Ladeskab
{
    public class Door:IDoor
    {
        private bool IsLocked { get; set; }
        public event EventHandler<DoorEventArgs> DoorEvent;
        public void OnDoorOpen()
        {
            Console.WriteLine("Tilslut Telefon");
        }

        public void OnDoorClose()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void LockDoor()
        {
            IsLocked = true;
            OnDoorChanged(new DoorEventArgs{ IsOpen = IsLocked });
        }

        public void UnlockDoor()
        {
            IsLocked = false;
            OnDoorChanged(new DoorEventArgs{ IsOpen = IsLocked });
        }

        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorEvent?.Invoke(this,e);
        }
        
    }
}
