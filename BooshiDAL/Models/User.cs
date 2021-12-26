using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActiveUser { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public UserDetails UserDetails { get; set; }
        [JsonIgnore]
        public List<Delivery> Deliveries { get; set; }
    }
}
