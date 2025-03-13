using BusinessLogicLayer.Common;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(registerModel);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(loginModel);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest();
        }

        [HttpPost("admin-register")]
        public async Task<IActionResult> RegisterAdmin(string email, string password)
        {
            var responce = await _userService.CreateAdmin(email, password);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _userService.ChangePassword(changePasswordDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var result = await _userService.DeleteAccount(email);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
