using System;
using System.Collections.Generic;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.StyleSyncProds
{
    public interface IStyleSyncProdService
    {
        Task<List<Model.StyleSyncProd>> GetList();
        Task<StyleSyncProd> Add(StyleSyncProdViewModel viewModel);
        Task Delete(int styleSyncProdId);
    }
}