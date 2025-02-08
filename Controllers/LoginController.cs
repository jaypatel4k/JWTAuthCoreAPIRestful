using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            IActionResult response;
            var user = AuthenticateUser(login);
            if (user == null)
            {
                var errorResponse = new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Access denied. Please provide valid credentials."
                };
                // Use StatusCode method to return 401 Unauthorized status and custom data
                return StatusCode(StatusCodes.Status401Unauthorized, errorResponse);

                // Returns a 401 Unauthorized response
                //response = Unauthorized(new { message = "Access denied. Please provide valid credentials." });
            }
            else
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }


        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim("Username", userInfo.Username),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserModel AuthenticateUser(LoginModel login)
        {
            UserModel userModel = null;
            if (login != null)
            {
                userModel = _userRepository.GetUser(login.Username, login.Password);
                if(userModel != null)
                {
                    userModel = new UserModel { Username = userModel.Username, Password = "", EmailAddress = userModel.EmailAddress, DateOfJoing = userModel.DateOfJoing };
                }
            }
            return userModel;
        }


    }
}
