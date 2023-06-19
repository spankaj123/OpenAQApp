using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary.Models
{
    public class AQResults
    {
        public Result[] results { get; set; }
    }
    public class Result
    {
        public string location { get; set; }
        public object city { get; set; }
        public string country { get; set; }
        public Measurement[] measurements { get; set; }
    }
    public class Measurement
    {
        public string parameter { get; set; }
        public float value { get; set; }
        public DateTime lastUpdated { get; set; }
        public string unit { get; set; }
    }
}
