using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public int DeliveryStatusId { get; set; }
        [JsonIgnore]
        public DeliveryStatus DeliveryStatus { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Origin Origin { get; set; }
        [JsonIgnore]
        public Destination Destination { get; set; }
    }
}
