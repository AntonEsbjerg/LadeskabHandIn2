using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.EventArgs;

namespace Ladeskab.RfidReaders
{
    interface IRfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;



    }
}
