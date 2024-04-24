using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JWTBearer.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EnsitechLibrary.Models;
using server.Models;

namespace JWTBearer.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase /*IUserRepository*/
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Object req) 
        {
            var data = JsonConvert.DeserializeObject<dynamic>(req.ToString());
            System.Console.WriteLine(data);
            
            string username = data.username.ToString(); 
            string password= data.password.ToString(); 
           
            var user = Models.User.GetUsers().Where(x => x.UserName == username && x.Password == password).FirstOrDefault();

            Console.WriteLine("user" + user);
            if (user == null ) 
            {
                //return "Incorrect Credentials";                 
                return NotFound();
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim("id", user.Id),
                new Claim("name", user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Rol),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(360),
                    signingCredentials: signIn
                ); 

            System.Console.WriteLine(token);
            System.Console.WriteLine(claims);
             return Ok(new  JwtSecurityTokenHandler().WriteToken(token));

        }

        [HttpPost]
        [Route("logout")]
        public IActionResult LogOut() 
        {
        //    HttpContext.Session.Clear();
            return Ok(new { message= "sesion ha sido cerrada"});      
        }
    } 

 
}
