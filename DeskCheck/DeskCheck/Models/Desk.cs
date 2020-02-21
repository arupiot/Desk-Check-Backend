using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeskCheck.Models
{
    public class Desk
    {
        public int deskID { get; set; }

        public float temperature { get; set; }

        public int CO2 { get; set; }

        public int floor { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public bool registered { get; set; }
    }
}
