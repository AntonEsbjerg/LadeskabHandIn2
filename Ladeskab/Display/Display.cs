using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Display:IDisplay
    {
        public void Print(string message)
        {
            Message = message;
            Console.WriteLine(message);
        }

        public string Message { get; set; }
    }
}
