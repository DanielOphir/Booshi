using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class UserDetails
    {
        [Key]
        public Guid UserId{ get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber{ get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string ZipCode { get; set; }

        [JsonIgnore]
        public User User { get; set; }

    }
}
