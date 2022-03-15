using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface IJsonFileModel
    {
        public uint Rfid { get; set; }
        public DateTime Time { get; set; }
        public bool IsDoorLocked { get; set; }
    }
}
