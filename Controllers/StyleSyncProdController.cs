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
        public async Task<IActionResult> Add(StyleSyncProdViewModel viewModel)
        {
            await _styleSyncProdService.Add(viewModel);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete (int styleSyncProdId)
        {
            await _styleSyncProdService.Delete(styleSyncProdId);
            return Ok();
        }
        
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll(int pageIndex, int pageSize, string searchQuery)
        {
            var result = await _styleSyncProdService.GetList();
            return Ok(result);
        }

    }
}