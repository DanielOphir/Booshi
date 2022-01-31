using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BooshiWebApi.Services
{
    public interface IJwtService
    {
        public Guid GetUserByTokenAsync(string jwtToken);
        public Guid GetUserByTokenAsync(HttpRequest request);
        public JwtSecurityToken Verify(string token);
        public Task<string> Generate(Guid id);
    }
}
