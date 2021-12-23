using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class DeliveryStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public List<Delivery> Deliveries { get; set; }
    }
}
