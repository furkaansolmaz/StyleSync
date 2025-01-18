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

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _loginService.Login(username, password);
            return Ok(result.Id);
        }
    }
}