using Microsoft.AspNetCore.Mvc;
using SessionCookie.API.DTOs;
using SessionCookie.API.Repositories;
using SessionCookie.API.Services;

namespace SessionCookie.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest logInRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userRepository.GetUserByUsernameAsync(logInRequest.UserName);
                if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(logInRequest.Password, user.PasswordHash))
                {
                    return Unauthorized("Invalid Username or Password.");
                }

                HttpContext.Session.SetString("Username", user.UserName);
                return Ok("Login Successful!");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequest register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterUserAsync(register);
            if(!result)
            {
                return BadRequest("Username is already taken.");
            }

            return Ok("User Registerd Successfully");
        }
    }
}
