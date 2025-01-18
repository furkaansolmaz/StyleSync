using Microsoft.EntityFrameworkCore;
using SyncStyle.DbContexts;
using SyncStyle.Model;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.StyleSyncProds
{

    public class StyleSyncProdService : IStyleSyncProdService
    {
        private readonly StyleSyncContext _styleSyncContext;
        public StyleSyncProdService(StyleSyncContext styleSyncContext)
        {
            _styleSyncContext = styleSyncContext;
        }

        public async Task<StyleSyncProd> Add(StyleSyncProdViewModel viewModel)
        {
            var styleSync = new StyleSyncProd()
            {
                UserId = viewModel.UserId,
                ImageUrl = viewModel.ImageUrl,
            };

			await _styleSyncContext.StyleSyncProds.AddAsync(styleSync);
            await _styleSyncContext.SaveChangesAsync();

            return styleSync;
        }
        public async Task Delete(int styleSyncProdId)
        {
            var styleSync = _styleSyncContext.StyleSyncProds.Where(i => i.StyleSyncProdId == styleSyncProdId && i.IsActive).FirstOrDefault();
            if (styleSync == null)
            {
                throw new Exception ("Silinecek Fotoğraf bilgisi bulunamadı.");
            }

			styleSync.IsActive = false;

			_styleSyncContext.StyleSyncProds.Attach(styleSync);
            await _styleSyncContext.SaveChangesAsync();
        }

        public async Task<List<StyleSyncProd>> GetList(int userId)
        {
             var list = await _styleSyncContext.StyleSyncProds.Where(ci => ci.IsActive && ci.UserId == userId).AsNoTracking().ToListAsync();
             if(list == null)
             {
                throw new Exception ("Listenecek Fotoğraf bilgisi bulunamadı.");
             }
             
             return list;
        }
    }
}