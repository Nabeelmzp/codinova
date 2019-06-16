using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CodinovaTask.Dto;
using CodinovaTask.Helper;
using CodinovaTask.Model;
using CodinovaTask.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodinovaTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
       // private readonly IMapper _mapper;
        private readonly AppSettings _appSettings; 
        public UsersController(IUserService userService,
            IOptions<AppSettings> appSettings
            )
        {
            _userService = userService;
            _appSettings = appSettings.Value;  
        }

        [AllowAnonymous]
        [HttpPost]  
        [Route("Login")]
        public IActionResult Login([FromBody]UserDto userDto)    
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password); 

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()) 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.UserId,  
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")] 
        public IActionResult Register([FromBody]UserDto userDto)  
        {
            // map dto to entity
            var user = userDto;

            User _obj = new User
            {  
                FirstName = userDto.FirstName,  
                LastName = userDto.LastName,
                Username = userDto.Username,    
            };


            try
            {
                // save 
                _userService.Create(_obj, userDto.Password);
                return Ok("Register Successfully");     
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }

}
