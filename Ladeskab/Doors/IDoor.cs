using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Doors;

namespace Ladeskab.Doors
{
    public interface IDoor
    {
        event EventHandler<EventArgs.DoorEventArgs> DoorEvent;
        public void OnDoorOpen();
        public void OnDoorClose();
        public void LockDoor();
        public void UnlockDoor();
    }
}
