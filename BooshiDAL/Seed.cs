using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL
{
    public static class Seed
    {

        private static List<(User user, UserDetails userDetails)> GetUsers()
        {
            var admin = new User
            {
                Id = new Guid(),
                UserName = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Email = "admin@booshi.com",
                RoleId = 1,
                IsActiveUser = true
            };
            var adminDetails = new UserDetails
            {
                User = admin,
                UserId = admin.Id,
                FirstName = "Admin",
                LastName = "Admin",
                City = "Tel Aviv",
                PhoneNumber = "0505700082",
                Street = "Even gvirol 53",
                ZipCode = "6436117"
            };

            var deliveryPerson = new User
            {
                Id = new Guid(),
                UserName = "delivery",
                Password = BCrypt.Net.BCrypt.HashPassword("delivery"),
                Email = "delivery@booshi.com",
                RoleId = 2,
                IsActiveUser = true
            };
            var deliveryPersonDetails = new UserDetails
            {
                User = deliveryPerson,
                UserId = deliveryPerson.Id,
                FirstName = "Delivery",
                LastName = "Delivery",
                City = "Tel Aviv",
                PhoneNumber = "0505700082",
                Street = "Even gvirol 53",
                ZipCode = "6436117"
            };

            var user = new User
            {
                Id = new Guid(),
                UserName = "user",
                Password = BCrypt.Net.BCrypt.HashPassword("user"),
                Email = "user@booshi.com",
                RoleId = 3,
                IsActiveUser = true
            };
            var userDetails = new UserDetails
            {
                User = user,
                UserId = user.Id,
                FirstName = "User",
                LastName = "User",
                City = "Tel Aviv",
                PhoneNumber = "0505700082",
                Street = "Even gvirol 53",
                ZipCode = "6436117"
            };

            var users = new List<(User user, UserDetails userDetails)>();
            users.Add((admin, adminDetails));
            users.Add((deliveryPerson, deliveryPersonDetails));
            users.Add((user, userDetails));
            return users;
        }

        /// <summary>
        /// Seeding the roles to database if not exists
        /// </summary>
        /// <param name="context">DB Context</param>
        /// <returns></returns>
        public static async Task SeedRoles(BooshiDBContext context)
        {
            if (await context.Roles.AnyAsync()) return;

            var roles = new List<Role>()
            {
                new Role() {RoleName = "Admin"},
                new Role() {RoleName = "DeliveryPerson"},
                new Role() {RoleName = "User"}
            };

            await context.Roles.AddRangeAsync(roles);
        }

        /// <summary>
        /// Seeding the users to database if not exists
        /// </summary>
        /// <param name="context">DB Context</param>
        /// <returns></returns>
        public static async Task SeedUsers(BooshiDBContext context)
        {
            if (await context.Users.AnyAsync() || await context.UsersDetails.AnyAsync()) return;

            var users = GetUsers();

            foreach (var user in users)
            {
                await context.Users.AddAsync(user.user);
                await context.UsersDetails.AddAsync(user.userDetails);
                if (user.user.RoleId == 2)
                {
                    await context.DeliveryPeople.AddAsync(new DeliveryPerson { UserId = user.user.Id, IsActiveDeliveryPerson = true });
                }
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Seeding the delivery statuses to database if not exists
        /// </summary>
        /// <param name="context">DB Context</param>
        /// <returns></returns>
        public static async Task SeedDeliveriesStatuses(BooshiDBContext context)
        {
            if (await context.DeliveryStatuses.AnyAsync()) return;

            var deliveryStatuses = new List<DeliveryStatus>()
            {
                new DeliveryStatus { Status = "Pending"},
                new DeliveryStatus { Status = "In Progress"},
                new DeliveryStatus { Status = "Completed"},
                new DeliveryStatus { Status = "Cancelled"}
            };

            await context.DeliveryStatuses.AddRangeAsync(deliveryStatuses);
            await context.SaveChangesAsync();
        }
    }
}
