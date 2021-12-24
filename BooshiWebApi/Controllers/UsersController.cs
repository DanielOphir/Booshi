using BooshiDAL;
using BooshiDAL.Models;
using BooshiWebApi.Models;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly BooshiDBContext _context;

        public UsersController(BooshiDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var usersList = _context.GetAllUsersAsync().Result;
            if (usersList.Count > 0)
            {
                return Ok(usersList);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _context.GetUserByIdAsync(Guid.Parse(id)).Result;
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(FullUser user)
        {
            var foundUser = await _context.UpdateUserAsync(user);
            if (foundUser.user != null)
            {
                return Ok(foundUser);
            }
            return BadRequest(foundUser.text);
        }

        [HttpGet("info/{id}")]
        public IActionResult GetUserInfoById(string id)
        {
            var user = _context.GetUserInfoById(Guid.Parse(id));
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromBody]Guid id)
        {
            var isDeleted = _context.DeleteUserByIdAsync(id);
            if (isDeleted.Result) return Ok("deleted");
            return BadRequest("id not found");
        }
    }
}
