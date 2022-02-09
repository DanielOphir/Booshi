using BooshiDAL.Interfaces;
using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BooshiDBContext _context;

        public UserRepository(BooshiDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Add user to the users table
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userDetails"></param>
        /// <returns>Returns the full user DTO that created.</returns>
        public async Task<FullUser> AddUserAsync(User user, UserDetails userDetails)
        {
            await _context.Users.AddAsync(user);
            userDetails.UserId = user.Id;
            await _context.UsersDetails.AddAsync(userDetails);
            await _context.SaveChangesAsync();
            var fullUser = new FullUser(user, userDetails);
            return fullUser;
        }

        /// <summary>
        /// Delete user from the users table
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns boolean wether if the method succeeded or not.</returns>
        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser != null)
            {
                _context.Users.Remove(foundUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all full users information by IQueryable format.
        /// </summary>
        /// <returns>List of all full users information by IQueryable format</returns>
        public IQueryable<FullUser> GetAllUsersQuery()
        {
            var usersList = _context.Users.Join(_context.UsersDetails, u => u.Id, u => u.UserId, (u, ud) => new FullUser(u, ud));
            return usersList;
        }

        /// <summary>
        /// Gets the user's role name
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns the users role name</returns>
        public async Task<string> GetRoleNameByUserIdAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            var role = _context.Roles.Where(role => role.Id == user.RoleId).Select(role => role.RoleName).SingleOrDefaultAsync();
            return await role;
        }

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns the user that matches the id, or null if not exists</returns>
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }

        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username">User username</param>
        /// <returns>Returns the user that matches the username, or null if not exists</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            return await user;
        }

        /// <summary>
        /// Gets full user details by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns the full user details that matches the id, or null if not exists</returns>
        public async Task<FullUser> GetUserInfoByIdAsync(Guid id)
        {
            var users = await GetAllUsersQuery().ToListAsync();
            var user = users.Where(user => user.Id == id).SingleOrDefault();
            return user;
        }

        /// <summary>
        /// Gets the number of users in Users table
        /// </summary>
        /// <returns>Returns int of users in Users table</returns>
        public int GetUsersCount(int roleId)
        {
            return GetAllUsersQuery().AsEnumerable().Where(user => user.RoleId == roleId).Count();
        }

        /// <summary>
        /// Checks if user exists by user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns boolean, true if exists, false if not exists</returns>
        public async Task<bool> isUserExistsAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user != null)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if certain email is already exists in the Users table
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Returns boolean, true if exists, false if not exists</returns>
        public async Task<bool> isEmailExistsAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user != null)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if certain username is already exists in the Users table
        /// </summary>
        /// <param name="email">User username</param>
        /// <returns>Returns boolean, true if exists, false if not exists</returns>
        public async Task<bool> isUsernameExistsAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == username);
            if (user != null)
                return true;
            return false;
        }

        /// <summary>
        /// Updates user in the Users table
        /// </summary>
        /// <param name="user">New user</param>
        /// <returns>Returns the updated user</returns>
        public async Task<FullUser> UpdateUserAsync(FullUser user)
        {
            var foundUserDetails = await _context.UsersDetails.FirstOrDefaultAsync(u => u.UserId == user.Id);
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (foundUserDetails == null || foundUser == null)
                return null;
            foundUserDetails.Street = user.Street;
            foundUserDetails.City = user.City;
            foundUserDetails.LastName = user.LastName;
            foundUserDetails.ZipCode = user.ZipCode;
            foundUserDetails.FirstName = user.FirstName;
            foundUserDetails.PhoneNumber = user.PhoneNumber;
            foundUser.Email = user.Email;
            foundUser.UserName = user.UserName;
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Gets the top 10 users by page number
        /// </summary>
        /// <param name="pageNum">Page number</param>
        /// <returns>Returns list of 10 or less users by page number</returns>
        public async Task<IEnumerable<FullUser>> GetUsersByPageAsync(int pageNum, int roleId)
        {
            var usersList = await GetAllUsersQuery().ToListAsync();
            var rtnUsers = usersList.Where(user => user.RoleId == roleId).Skip(pageNum * 10 - 10).Take(10);
            return rtnUsers;
        }

        public async Task<IEnumerable<FullUser>> GetUsersByUsernameByPageAsync(string userName, int pageNum, int roleId)
        {

            var usersList = await GetAllUsersQuery().ToListAsync();
            return usersList.Where(user => user.UserName.Contains(userName) && user.RoleId == roleId).Skip(pageNum * 10 - 10).Take(10);

        }

        public async Task<int> GetUsersByUsernameCount(string userName, int roleId)
        {
            var usersList = await GetAllUsersQuery().ToListAsync();
            return usersList.Where(user => user.UserName.Contains(userName) && user.RoleId == roleId).Count();
        }

        public async Task<bool> ChangeUserPassword(Guid id, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user == null)
            {
                return false;
            }
            user.Password = password;
            user.TempPassword = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetTempPassword(Guid id, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user == null)
            {
                return false;
            }
            user.TempPassword = password;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
