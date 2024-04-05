using Microsoft.AspNetCore.Mvc;
using API_Stock.Models;
using API_Stock.Services;
using API_Stock.ViewModes;

namespace API_Stock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        //https://localhost:port/api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                User? user = await _userService.Register(registerViewModel);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                //Trả về lỗi {message: "Nội dung lỗi"}
                return BadRequest(new {Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                string jwtToken = await _userService.Login(loginViewModel);
                return Ok(new { jwtToken });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {Message = ex.Message });
            }
        }
    }
}
