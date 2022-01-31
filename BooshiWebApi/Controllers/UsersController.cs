using BooshiDAL;
using BooshiDAL.Interfaces;
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
    [Authorize(Roles = "Admin, DeliveryPerson")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this._userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await _userRepo.GetAllUsersQuery().ToListAsync();
            if (usersList.Count > 0)
            {
                return Ok(usersList);
            }
            return NoContent();
        }

        [HttpGet("page/{pageNum}")]
        public async Task<IActionResult> GetUsersByPage(int pageNum)
        {
            var users = await _userRepo.GetUsersByPageAsync(pageNum);
            if (users.Count() > 0)
            {
                var totalCount = _userRepo.GetUsersCount();
                return Ok(new { users, totalCount});
            }
            return NoContent();
        }

        [HttpGet("{userName}/page/{pageNum}")]
        public async Task<IActionResult> GetUsersByUserNameByPage(string userName, int pageNum)
        {
            var users = await _userRepo.GetUsersByUsernameByPageAsync(userName, pageNum);
            if (users.Count() > 0)
            {
                var totalCount = _userRepo.GetUsersCount();
                return Ok(new { users, totalCount });
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepo.GetUserInfoByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateUser(FullUser user)
        {
            var foundUser = await _userRepo.GetUserByIdAsync(user.Id);
            if (foundUser == null)
            {
                return NotFound("User not found");
            }
            if ((user.UserName != foundUser.UserName) && await _userRepo.isUsernameExistsAsync(user.UserName))
                return BadRequest(new { type = "username", message = "This username is already exists." });
            if ((user.Email != foundUser.Email) && await _userRepo.isEmailExistsAsync(user.Email))
                return BadRequest(new { type = "email", message = "A user with this email is already exists." });
            await _userRepo.UpdateUserAsync(user);
            return Ok(foundUser);
        }

        [HttpGet("info/{id}")]
        public IActionResult GetUserInfoById(Guid id)
        {
            var user = _userRepo.GetUserInfoByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromBody]Guid id)
        {
            var isDeleted = _userRepo.DeleteUserByIdAsync(id);
            if (isDeleted.Result) return Ok("deleted");
            return BadRequest("id not found");
        }
    }
}
