using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.ChatGpts
{
    public interface IChatGptService
    {
        Task<ChatGbtResponseViewModel> ChatGptRequest(ChatGbtRequestViewModel viewModel);
    }
}