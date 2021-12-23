using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL
{
    public partial class BooshiDBContext
    {
        /// <summary>
        /// Asynchronous method that gets the username of user and returns the user if found, if not, return null.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>User/null</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = this.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return await user;
        }

        /// <summary>
        /// Asynchronous method that gets the id of user and returns the user if found, if not, return null.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await this.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// Method that gets the id of user and returns the full user information if found, else returns null.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>If found, returns the full user information, else returns null</returns>
        public FullUser GetUserInfoById(Guid id)
        {
            var user = this.Users.Join(this.UsersDetails, u => u.Id, u => u.UserId, (u, ud) => new
            FullUser
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                RoleId = u.RoleId,
                FirstName = ud.FirstName,
                LastName = ud.LastName,
                City = ud.City,
                ZipCode = ud.ZipCode,
                PhoneNumber = ud.PhoneNumber,
                Street = ud.Street
            }).ToList().FirstOrDefault(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// Asynchronous method that update user.
        /// </summary>
        /// <param name="user">Full user model</param>
        /// <returns>returns the user and a error/success message</returns>
        public async Task<(FullUser user, string text)> UpdateUserAsync(FullUser user)
        {
            var foundUserDetails = await this.UsersDetails.FirstOrDefaultAsync(u => u.UserId == user.Id);
            var foundUser = await this.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (foundUserDetails == null || foundUser == null)
                return (null, "No user found");
            foundUserDetails.Street = user.Street;
            foundUserDetails.City = user.City;
            foundUserDetails.LastName = user.LastName;
            foundUserDetails.ZipCode = user.ZipCode;
            foundUserDetails.FirstName = user.FirstName;
            foundUserDetails.PhoneNumber = user.PhoneNumber;
            foundUser.Email = user.Email;
            foundUser.RoleId = user.RoleId;
            try
            {
                await this.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (null, ex.InnerException.Message);
            }
            return (user, "Success");
        }

        /// <summary>
        /// Asynchronous Method that delete user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>boolean wether if the method succeeded or not.</returns>
        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var foundUser = await this.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser != null)
            {
                this.Users.Remove(foundUser);
                await this.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method that gets all full users information.
        /// </summary>
        /// <returns>List of all full users information</returns>
        public async Task<List<FullUser>> GetAllUsersAsync()
        {
            var usersList = this.Users.Join(this.UsersDetails, u => u.Id, u => u.UserId, (u, ud) => new FullUser{
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                RoleId = u.RoleId,
                FirstName = ud.FirstName,
                LastName = ud.LastName,
                City = ud.City,
                ZipCode = ud.ZipCode,
                PhoneNumber = ud.PhoneNumber,
                Street = ud.Street
            });
            return await usersList.ToListAsync();
        }

        public string GetRoleNameByUserId(Guid id)
        {
            var user = GetUserByIdAsync(id).Result;
            var role = this.GetRoleNameById(user.RoleId);
            return role;
        }
    }
}
