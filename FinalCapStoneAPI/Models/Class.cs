using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapStoneAPI.Models
{

    public class ListOfCars
    {
        public CarFromList[] Property1 { get; set; }
    }

    public class CarFromList
    {
        public int id { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string color { get; set; }
    }

}
