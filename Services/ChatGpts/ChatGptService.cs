using Microsoft.EntityFrameworkCore;
using OpenAI;
using OpenAI.Chat;
using SyncStyle.DbContexts;
using SyncStyle.OpenWeatherMaps;
using SyncStyle.ViewModel;

namespace SyncStyle.ChatGpts
{
    public class ChatGptService : IChatGptService
    {
        private readonly StyleSyncContext _styleSyncContext;
        private readonly IWeatherService _weatherService;


        public ChatGptService(StyleSyncContext styleSyncContext,
                            IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _styleSyncContext = styleSyncContext;
        }

        public async Task<ChatGbtResponseViewModel> ChatGptRequest(ChatGbtRequestViewModel viewModel)
        {
            string key = "chatgptApiKey";
            OpenAIClient client = new OpenAIClient(key);

            var content = new List<Content>
            {
                new Content
                (input: viewModel.InformationRequest)
            };

            var styleSyncProd = await _styleSyncContext.StyleSyncProds.Where(i => viewModel.StyleSyncProdId.Contains(i.StyleSyncProdId)).ToListAsync();

            var location = await _weatherService.GetWeatherAsync(viewModel.City);

            if(styleSyncProd == null)
            {
                throw new Exception("Görsel eklemeniz gerekmektedir.");
            }
            content.AddRange(styleSyncProd.Select(
                i => new Content
                    (imageUrl: new ImageUrl(url: "data:image/png;base64," + i.Image)))
            );

            content.Add(new Content(input : location));

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