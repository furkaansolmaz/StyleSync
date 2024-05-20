using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.ChatGpts
{
    public interface IChatGptService
    {
        Task<ChatGbtResponseViewModel> ChatGptRequest(ChatGbtRequestViewModel viewModel);
    }
}