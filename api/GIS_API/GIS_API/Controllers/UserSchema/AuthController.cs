using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GIS_API.Managers.Users;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GIS_API.Controllers.UserSchema
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IUserManager userManager;

        public AuthController(IConfiguration config, IUserManager userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        /// <summary>
        /// Register method.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>Created status code.</returns>
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> RegisterUser(UserViewModel user)
        {
            await this.userManager.Register(user);
            return Created("User registered", user);
        }

        /// <summary>
        /// Login method.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>Token.</returns>
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginUser(LoginViewModel user)
        {
            var userModel = await this.userManager.Login(user);
            if (userModel == null)
            {
                return Unauthorized("Login error: User Not Found");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, userModel.Email),
                new Claim("isAdmin", userModel.IsAdmin.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(this.config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(300),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                isAdmin = userModel.IsAdmin
            });
        }

    }
}