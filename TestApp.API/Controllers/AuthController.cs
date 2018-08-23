namespace TestApp.API.Controllers {
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using TestApp.API.Data.Repositories.Interfaces;
    using TestApp.API.DTOs;
    using TestApp.API.Models;

    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IUserRepository repository;

        private readonly IConfiguration config;

        public AuthController (IUserRepository userRepository, IConfiguration configuration) {
            this.repository = userRepository;
            this.config = configuration;
        }

        [HttpPost, Route ("register")]
        public async Task<IActionResult> Register (RegisterUser user) {
            user.Username = user.Username.ToLower ();

            if (await repository.UserExists (user.Username))
                return BadRequest ("A user with the same username already exists");

            var toRegister = new User {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                EmailAddress = user.EmailAddress,
                Username = user.Username
            };

            await repository.Register (toRegister, user.Password);

            return Ok (string.Format ("Successfully registered {0}", user.Username));
        }

        [HttpPost, Route ("login")]
        public async Task<IActionResult> Login (UserToLogin userToLogin) {
            var user = await repository.Login (userToLogin.Username.ToLower(), userToLogin.Password);

            if (user == null)
                return Unauthorized ();

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ClaimTypes.Email, user.EmailAddress),
                new Claim (ClaimTypes.GivenName, string.Format ("{0} {1}", user.Firstname, user.Lastname))
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (config.GetSection ("AppSettings:Token").Value));
            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var token = tokenHandler.CreateToken (tokenDescriptor);

            return Ok (new { token = tokenHandler.WriteToken (token) });
        }
    }
}