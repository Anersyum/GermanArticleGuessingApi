using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Ines_German.API.Data;
using Ines_German.API.Dtos;
using Ines_German.API.Helpers;
using Ines_German.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ines_German.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;
        private readonly IConfiguration config;
        public AuthController(IAuthRepository authRepo, IConfiguration config)
        {
            this.config = config;
            this.authRepo = authRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAction(PasswordDto passwordDto)
        {
            UserModel user = await this.authRepo.Login(passwordDto.Password);

            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Wrong password!"
                });
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtHandlerCreator tokenCreator = new JwtHandlerCreator(this.config);

            var token = tokenCreator.CreateToken(tokenHandler, user);

            return Ok(new 
            {
                success = true,
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}