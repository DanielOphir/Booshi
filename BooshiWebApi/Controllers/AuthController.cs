using BooshiDAL;
using BooshiDAL.Extensions;
using BooshiDAL.Interfaces;
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
    public class AuthController : BaseController
    {
        private readonly BooshiDBContext _context;
        private readonly IJwtService _jwtService;
        private readonly MailService _mailService;
        private readonly IUserRepository _userRepo;

        public AuthController(BooshiDBContext context, IJwtService jwtService,
            MailService mailService, IUserRepository userRepo)
        {
            this._context = context;
            this._jwtService = jwtService;
            this._mailService = mailService;
            this._userRepo = userRepo;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            if (await _userRepo.isUsernameExistsAsync(registerModel.UserName))
                return BadRequest(new {type="username", message="This username is already exists."});
            if (await _userRepo.isEmailExistsAsync(registerModel.Email))
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
                await _userRepo.AddUserAsync(newUser, newUserDetails);
               _mailService.SendMail(newUser.Email, "Registration complete", "Thank you for signing up to our delivery service, we are excited to work along with you.");
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
            var user = await _userRepo.GetUserByUsernameAsync(loginModel.UserName);
            if (user == null)
                return BadRequest(new {message= "Either your username or password is incorrect" });
            if (user.TempPassword != null && !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
            {
                if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.TempPassword))
                    return BadRequest(new { message = "Either your username or password is incorrect" });
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
                    return BadRequest(new { message = "Either your username or password is incorrect" });
            }
            var token = await _jwtService.Generate(user.Id);
            return Ok(new {token});
        }

         [HttpGet("user")]
         public async Task<IActionResult> GetUser()
         {
            var userId = _jwtService.GetUserByTokenAsync(Request);
            var user = await _userRepo.GetUserByIdAsync(userId);
            return Ok(user);
         }

        [HttpGet("fulluser")]
        public async Task<IActionResult> GetFullUser()
        {
            FullUser user;
            var userId = _jwtService.GetUserByTokenAsync(Request);
            user = await _userRepo.GetUserInfoByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]string password)
        {
            var userId = _jwtService.GetUserByTokenAsync(Request);
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var isSuccess = await _userRepo.ChangeUserPassword(userId, encryptedPassword);
            if (isSuccess)
                return Ok();
            return BadRequest();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> GetRandomPassword([FromBody]string userName)
        {
            var user = await _userRepo.GetUserByUsernameAsync(userName);
            if (user == null)
            {
                return Ok();
            }
            var tempPassword = HelperMethods.GenerateRandomPassword(8);
            var encryptedTempPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);
            var isSuccess = await _userRepo.SetTempPassword(user.Id, encryptedTempPassword);
            if (!isSuccess)
                return BadRequest("Something went wrong.");
            try
            {
                _mailService.SendMail(user.Email, "New password", $"Your temp password is - {tempPassword}");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
            return Ok();
        }

    }
}