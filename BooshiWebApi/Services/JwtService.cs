using BooshiDAL;
using BooshiDAL.Interfaces;
using BooshiDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BooshiWebApi.Services
{
    public class JwtService : IJwtService
    {

        private string secureKey = "Booshi is the best delivery system";
        private readonly IUserRepository _userRepo;

        public JwtService(IUserRepository userRepo)
        {
            this._userRepo = userRepo;
        }
        public async Task<string> Generate(Guid id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var user = _userRepo.GetUserByIdAsync(id).Result;
            var header = new JwtHeader(credentials);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, await _userRepo.GetRoleNameByUserIdAsync(id))
            };

            var payload = new JwtPayload(issuer:id.ToString(), null, claims, null, DateTime.UtcNow.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secureKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken) validatedToken;
        }
        public Guid GetUserByTokenAsync(HttpRequest request)
        {
            var jwtToken = request.Headers["Authorization"].ToString().Substring(7);
            JwtSecurityToken token;
            token = this.Verify(jwtToken);
            Guid userId = Guid.Parse(token.Issuer);
            return userId;
        }
        public Guid GetUserByTokenAsync(string jwtToken)
        {
            JwtSecurityToken token;
            token = this.Verify(jwtToken);
            Guid userId = Guid.Parse(token.Issuer);
            return userId;
        }
    }
}
