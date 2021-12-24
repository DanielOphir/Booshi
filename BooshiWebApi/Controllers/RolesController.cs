using BooshiDAL;
using BooshiDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly BooshiDBContext _context;

        public RolesController(BooshiDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _context.GetRolesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var role = await _context.AddRole(roleName);
            return Ok(role);
        }
    }
}
