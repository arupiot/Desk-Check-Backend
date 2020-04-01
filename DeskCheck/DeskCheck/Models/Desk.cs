using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskCheck.Models
{
    public class Desk
    {
        [JsonProperty("deskID")]
        public int deskID { get; set; }

        [JsonProperty("temp")]
        public float temp { get; set; }

        [JsonProperty("CO2")]
        public int CO2 { get; set; }

        [JsonProperty("floor")]
        public int floor { get; set; }

        [JsonProperty("X")]
        public float X { get; set; }

        [JsonProperty("Y")]
        public float Y { get; set; }

        [JsonProperty("available")]
        public bool available { get; set; }
    }
}
