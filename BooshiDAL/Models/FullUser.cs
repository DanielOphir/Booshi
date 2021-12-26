using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Models
{
    public class FullUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public bool isActiveUser { get; set; }

        public FullUser()
        {

        }

        public FullUser(User user, UserDetails userDetails)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.RoleId = user.RoleId;
            this.FirstName = userDetails.FirstName;
            this.LastName = userDetails.LastName;
            this.PhoneNumber = userDetails.PhoneNumber;
            this.City = userDetails.City;
            this.Street = userDetails.Street;
            this.ZipCode = userDetails.ZipCode;
            this.isActiveUser = user.IsActiveUser;
        }
    }
}
