using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.EventArgs
{
    public class RfidEventArgs : System.EventArgs
    {
            public uint Rfid { get; set; }
            public DateTime Time { get; set; }
    }
}
