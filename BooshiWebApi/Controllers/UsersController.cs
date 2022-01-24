using BooshiDAL;
using BooshiDAL.Models;
using BooshiWebApi.Models;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await _context.GetAllUsersQuery().ToListAsync();
            if (usersList.Count > 0)
            {
                return Ok(usersList);
            }
            return NoContent();
        }

        [HttpGet("page/{pageNum}")]
        public async Task<IActionResult> GetUsersByPage(int pageNum)
        {
            var users = await _context.GetUsersByPageAsync(pageNum);
            if (users.Count > 0)
            {
                var totalCount = _context.GetUsersCount();
                return Ok(new { users, totalCount});
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _context.GetUserInfoById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateUser(FullUser user)
        {
            var foundUser = await _context.GetUserByIdAsync(user.Id);
            if (foundUser == null)
            {
                return NotFound("User not found");
            }
            if ((user.UserName != foundUser.UserName) && await _context.isUsernameExistsAsync(user.UserName))
                return BadRequest(new { type = "username", message = "This username is already exists." });
            if ((user.Email != foundUser.Email) && await _context.isEmailExistsAsync(user.Email))
                return BadRequest(new { type = "email", message = "A user with this email is already exists." });
            await _context.UpdateUserAsync(user);
            return Ok(foundUser);
        }

        [HttpGet("info/{id}")]
        public IActionResult GetUserInfoById(Guid id)
        {
            var user = _context.GetUserInfoById(id);
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
