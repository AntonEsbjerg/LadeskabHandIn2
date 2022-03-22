using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.EventArgs;

namespace Ladeskab.RfidReaders
{
    public class RfidReader:IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;

        public void ReadRfid(uint id, DateTime time)
        {
            OnRfidChanged(new RfidEventArgs { Rfid = id, Time = time});
        }

        protected virtual void OnRfidChanged(RfidEventArgs e)
        {
            RfidEvent?.Invoke(this, e);
        }
    }
}
