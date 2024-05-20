using Microsoft.AspNetCore.Mvc;
using SyncStyle.Services.ChatGpts;
using SyncStyle.Services.Members;
using SyncStyle.ViewModel;

namespace SyncStyle.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatGptServiceController : ControllerBase
    {
        private readonly IChatGptService _chatGptService;
        public ChatGptServiceController(IChatGptService chatGptService)
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