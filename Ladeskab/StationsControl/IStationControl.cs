using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IStationControl
    {
        public void DoorOpened();
        public void DoorClosed();
        public void RFIDDetected(uint id);
        public bool CheckId(uint oldId, uint id);
    }
}
