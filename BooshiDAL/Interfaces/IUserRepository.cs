using BooshiDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooshiDAL.Interfaces
{
    public interface IUserRepository
    {
        public IQueryable<FullUser> GetAllUsersQuery();
        public Task<bool> isUserExistsAsync(Guid id);
        public  Task<bool> isUsernameExistsAsync(string username);
        public Task<bool> isEmailExistsAsync(string email);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<FullUser> GetUserInfoByIdAsync(Guid id);
        public Task<FullUser> UpdateUserAsync(FullUser user);
        public Task<bool> DeleteUserByIdAsync(Guid id);
        public Task<FullUser> AddUserAsync(User user, UserDetails userDetails);
        public Task<string> GetRoleNameByUserIdAsync(Guid id);
        public Task<IEnumerable<FullUser>> GetUsersByPageAsync(int pageNum, int roleId);
        public Task<IEnumerable<FullUser>> GetUsersByUsernameByPageAsync(string userName, int pageNum, int roleId);
        public Task<int> GetUsersByUsernameCount(string userName, int roleId);

        public Task<bool> ChangeUserPassword(Guid id, string password);
        public Task<bool> SetTempPassword(Guid id, string password);
        public int GetUsersCount(int roleId);

    }
}
