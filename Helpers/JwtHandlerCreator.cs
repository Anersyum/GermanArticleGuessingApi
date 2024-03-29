using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ines_German.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ines_German.API.Helpers
{
    public class JwtHandlerCreator
    {
        private readonly IConfiguration config;
        public JwtHandlerCreator(IConfiguration config)
        {
            this.config = config;

        }
        
        public SecurityToken CreateToken(JwtSecurityTokenHandler tokenHandler, UserModel user)
        {
            // we build the token with claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Role),
            };

            // here we sign the token so that we can authenticate the token on the server side
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // creating the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}