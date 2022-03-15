using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    interface IStationControl
    {
        public void DoorOpened();
        public void DoorClosed();
        public void RFIDDetected(int id);
        public bool CheckId(int oldId, int Id);
    }
}
