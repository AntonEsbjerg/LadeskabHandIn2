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
            if (!IsLocked)
            {
                OnDoorChanged(new DoorEventArgs { IsOpen = true });
            }
        }

        public void OnDoorClose()
        {
            OnDoorChanged(new DoorEventArgs { IsOpen = false });
        }

        public void LockDoor()
        {
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            IsLocked = false;
        }

        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorEvent?.Invoke(this,e);
        }
        
    }
}
