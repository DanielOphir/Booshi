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

        public object GetClaims { get; private set; }

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

            var newUser = new User
            {
                Id = new Guid(),
                UserName = registerModel.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Email = registerModel.Email,
                RoleId = registerModel.RoleId,
                Role = _context.Roles.FirstOrDefault(r => r.Id == registerModel.RoleId)
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
               await _context.Users.AddAsync(newUser);
               await _context.UsersDetails.AddAsync(newUserDetails);
               await _context.SaveChangesAsync();
                _mailService.SendMail(newUser.Email, "Registration complete", "Ty for signing up to our delivery service, we wish you a great.");
               return Created("", newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "Exception : " + ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginModel loginModel)
        {
            var user = await _context.GetUserByUsernameAsync(loginModel.UserName);
            if (user == null)
            {
                return BadRequest("Either your username or password is incorrect");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            {
                return BadRequest("Either your username or password is incorrect");
            }
            var token = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(new {token});
        }

         [HttpGet("user")]
         public async Task<IActionResult> GetUser()
        {
            User user;
            var jwtToken = Request.Cookies["jwt"];
            if (jwtToken == null)
                return StatusCode(203);
            try { 
                var userId = _jwtService.GetUserByTokenAsync(jwtToken);
                user = await _context.GetUserByIdAsync(userId);
            }
            catch (SecurityTokenExpiredException) {
                Response.Cookies.Delete("jwt");
                return StatusCode(203);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
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
