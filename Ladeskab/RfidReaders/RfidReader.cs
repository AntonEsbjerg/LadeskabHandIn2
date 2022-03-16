﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.EventArgs;

namespace Ladeskab.RfidReaders
{
    public class RfidReader
    {
        public event EventHandler<RfidEventArgs> RfidEvent;
      


        public void ReadRfid(uint id)
        {
            OnRfidChange(new RfidEventArgs { Rfid = id });
        }



        protected virtual void OnRfidChange(RfidEventArgs e)
        {
            RfidEvent?.Invoke(this, e);
        }
    }
}
