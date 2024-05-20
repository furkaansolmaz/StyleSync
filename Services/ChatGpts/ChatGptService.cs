using Microsoft.EntityFrameworkCore;
using OpenAI;
using OpenAI.Chat;
using SyncStyle.DbContexts;
using SyncStyle.ViewModel;

namespace SyncStyle.ChatGpts
{
    public class ChatGptService : IChatGptService
    {
        private readonly StyleSyncContext _styleSyncContext;
        public ChatGptService(StyleSyncContext styleSyncContext)
        {
            _styleSyncContext = styleSyncContext;
        }

        public async Task<ChatGbtResponseViewModel> ChatGptRequest(ChatGbtRequestViewModel viewModel)
        {
            
            OpenAIClient client = new OpenAIClient("sk-proj-cxbRWqYJTTKyzYUtmEzfT3BlbkFJ6aIj5VRBtOBuNMS5bnBg");

            var content = new List<Content>
            {
                new Content
                (input: viewModel.InformationRequest)
            };

            var styleSyncProd = await _styleSyncContext.StyleSyncProds.Where(i => viewModel.StyleSyncProdId.Contains(i.StyleSyncProdId)).ToListAsync();

            if(styleSyncProd == null)
            {
                throw new Exception("Görsel eklemeniz gerekmektedir.");
            }
            content.AddRange(styleSyncProd.Select(
                i => new Content
                    (imageUrl: new ImageUrl(url: i.Image)))
            );

            var messages = new List<Message>
            {
                new Message(role: Role.User, content: content)
            };

            var request = new ChatRequest
            (
                model: "gpt-4o",
                messages: messages,
                maxTokens: 300
            );

            var response = await client.ChatEndpoint.GetCompletionAsync(request);

            return new ChatGbtResponseViewModel
            {
                InformationResponse = response.Choices[0]
            };

        }
    }
}