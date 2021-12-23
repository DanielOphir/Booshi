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
    }
}
