using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskCheck.Models
{
    public class Desk
    {
        public int deskID { get; set; }

        public int temperature { get; set; }

        public int CO2 { get; set; }

        public int floor { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool registered { get; set; }
    }
}
