using Microsoft.AspNetCore.Mvc;
using SyncStyle.Services.Logins;
using SyncStyle.ViewModel;

namespace SyncStyle.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var result = await _loginService.Login(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            try
            {
                var result = await _loginService.ResetPassword(model.UserName, model.Email);
                return Ok(new { message = "Password reset instructions have been sent to your email." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}