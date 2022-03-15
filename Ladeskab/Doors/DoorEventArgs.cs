using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Doors
{
    public class DoorEventArgs : EventArgs
    {
        public bool IsOpen { get; set; }
    }
}
