using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarDynamicObservatoryWebService
{
     public class Product
    {
        public string Date { get; set; }
        public string UtcTime { get; set; }
        public string  Resolution { get; set; }
        public string Wavelength { get; set; }
        public string Url { get; set; }
    }
}
