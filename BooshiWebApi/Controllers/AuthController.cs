using BooshiDAL;
using BooshiDAL.Models;
using BooshiWebApi.Models;
using BooshiWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BooshiWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BooshiDBContext _context;
        private readonly JwtService _jwtService;
        private readonly MailService _mailService;

        public AuthController(BooshiDBContext context, JwtService jwtService,
            MailService mailService)
        {
            this._context = context;
            this._jwtService = jwtService;
            this._mailService = mailService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            if (await _context.isUsernameExistsAsync(registerModel.UserName))
                return BadRequest(new {type="username", message="This username is already exists."});
            if (await _context.isEmailExistsAsync(registerModel.Email))
                return BadRequest(new { type = "email", message = "A user with this email is already exists." });
            var newUser = new User
            {
                Id = new Guid(),
                UserName = registerModel.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Email = registerModel.Email,
                RoleId = registerModel.RoleId,
                IsActiveUser = true
            };
            var newUserDetails = new UserDetails
            {
                User = newUser,
                UserId = newUser.Id,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                City = registerModel.City,
                PhoneNumber = registerModel.PhoneNumber,
                Street = registerModel.Street,
                ZipCode = registerModel.ZipCode
            };
            try
            {
                await _context.AddUserAsync(newUser, newUserDetails);
                //_mailService.SendMail(newUser.Email, "Registration complete", "Ty for signing up to our delivery service, we wish you a great.");
               return Created("", newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "Exception : " + ex.InnerException.Message});
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginModel loginModel)
        {
            var user = await _context.GetUserByUsernameAsync(loginModel.UserName);
            if (user == null)
            {
                return BadRequest(new {message= "Either your username or password is incorrect" });
            }
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            {
                return BadRequest(new { message = "Either your username or password is incorrect" });
            }
            var token = _jwtService.Generate(user.Id);
            return Ok(new {token});
        }

         [HttpGet("user")]
         public async Task<IActionResult> GetUser()
        {
            User user;
            var jwtToken = Request.Headers["Authorization"].ToString().Substring(7);
            var userId = _jwtService.GetUserByTokenAsync(jwtToken);
            user = await _context.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpGet("fulluser")]
        public async Task<IActionResult> GetFullUser()
        {
            FullUser user;
            var jwtToken = Request.Headers["Authorization"].ToString().Substring(7);
            var userId = _jwtService.GetUserByTokenAsync(jwtToken);
            user = await _context.GetUserInfoById(userId);
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt");
                return Ok("logged out");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}