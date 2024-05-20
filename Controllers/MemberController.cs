using Microsoft.AspNetCore.Mvc;
using SyncStyle.Services.Members;
using SyncStyle.ViewModel;

namespace SyncStyle.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(MemberViewModel viewModel)
        {
            await _memberService.Add(viewModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int memberId)
        {
            await _memberService.Delete(memberId);
            return Ok();
        }
    }
}