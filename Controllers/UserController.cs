using EmployeeAuthentication.Models;
using EmployeeAuthentication.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAuthentication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationRequest request)
        {
            if (_userService.Register(request.Username, request.Password))
            {
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("User already exists.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            if (_userService.Login(request.Username, request.Password))
            {
                return Ok("Login successful.");
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}

