using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class FullDelivery
    {
        public Delivery Delivery { get; set; }
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
    }
}
