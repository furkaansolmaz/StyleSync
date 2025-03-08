using Microsoft.AspNetCore.Mvc;
using SyncStyle.Services.StyleSyncProds;
using SyncStyle.ViewModel;

namespace SyncStyle.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StyleSyncProdController : ControllerBase
    {
        private readonly IStyleSyncProdService _styleSyncProdService;
        public StyleSyncProdController(IStyleSyncProdService styleSyncProdService)
        {
            _styleSyncProdService = styleSyncProdService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StyleSyncProdViewModel viewModel, [FromForm] IFormFile imageFile)
        {
            var result = await _styleSyncProdService.Add(viewModel, imageFile);
            return Ok(result.StyleSyncProdId);
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete (int styleSyncProdId)
        {
            await _styleSyncProdService.Delete(styleSyncProdId);
            return Ok();
        }
        
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll(int memberId)
        {
            var result = await _styleSyncProdService.GetList(memberId);
            return Ok(result);
        }

    }
}