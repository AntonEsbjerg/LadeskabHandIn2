using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Display: IDisplay
    {
       public StringWriter _sw { get; set; }
        public Display(StringWriter sw)
        {
            _sw = sw;
        }
        public void Print(string message)
        {
            Message = message;
            _sw.Write(message);
        }

        public string Message { get; set; }
    }
}
