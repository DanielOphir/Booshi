using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class DeliveryPerson
    {
        [Key]
        public Guid UserId { get; set; }
        public bool IsActiveDeliveryPerson { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }
        [JsonIgnore]
        public List<Delivery> Deliveries { get; set; }
    }
}