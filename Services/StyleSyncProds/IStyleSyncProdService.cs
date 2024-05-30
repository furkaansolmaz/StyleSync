using System;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.StyleSyncProds
{
    public interface IStyleSyncProdService
    {
        Task<List<StyleSyncProd>> GetList(int memberId);
        Task<StyleSyncProd> Add(StyleSyncProdViewModel viewModel);
        Task Delete(int styleSyncProdId);
    }
}