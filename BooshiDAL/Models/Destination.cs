using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class Destination
    {
        [Key]
        public int DeliveryId { get; set; }
        [JsonIgnore]
        public Delivery Delivery { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ReciverFirstName { get; set; }
        public string ReciverLastName { get; set; }
        public string RevicerPhoneNumber { get; set; }
        public string RevicerEmail { get; set; }
    }
}
