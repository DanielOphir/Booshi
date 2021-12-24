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
        public string GetRoleNameById(int id)
        {
            return this.Roles.Where(r => r.Id == id).Select(r => r.RoleName).First();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await this.Roles.ToListAsync();
        }

        public async Task<Role> AddRole(string roleName)
        {
            var role = await this.Roles.AddAsync(new Role { RoleName = roleName});
            await this.SaveChangesAsync();
            return role.Entity;
        }
    }
}
