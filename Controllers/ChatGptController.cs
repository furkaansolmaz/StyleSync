using Microsoft.AspNetCore.Mvc;
using SyncStyle.ChatGpts;
using SyncStyle.ViewModel;

namespace SyncStyle.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;
        public ChatGptController(IChatGptService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        [HttpPost]
        public async Task<ChatGbtResponseViewModel> RequestChatCpt(ChatGbtRequestViewModel viewModel)
        {
            var result = await _chatGptService.ChatGptRequest(viewModel);
            return result;
        }
    }
}