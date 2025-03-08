using Microsoft.EntityFrameworkCore;
using SyncStyle.DbContexts;
using SyncStyle.Model;
using SyncStyle.Services.AmazonS3;
using SyncStyle.ViewModel;

namespace SyncStyle.Services.StyleSyncProds
{

    public class StyleSyncProdService : IStyleSyncProdService
    {
        private readonly StyleSyncContext _styleSyncContext;
        private readonly IS3Service _s3Service;

        public StyleSyncProdService(StyleSyncContext styleSyncContext,
                                    IS3Service s3Service)
        {
            _styleSyncContext = styleSyncContext;
            _s3Service = s3Service;
        }

        public async Task<StyleSyncProd> Add(StyleSyncProdViewModel viewModel, IFormFile imageFile)
        {
            string imageUrl = await _s3Service.UploadImageToS3Async(imageFile);

            var styleSync = new StyleSyncProd()
            {
                UserId = viewModel.UserId,
                ImageUrl = imageUrl,
                IsActive = true,
            };

            try
            {
                await _styleSyncContext.StyleSyncProds.AddAsync(styleSync);
                await _styleSyncContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add StyleSyncProd: {ex.Message}");
            }

            return styleSync;
        }

        public async Task Delete(int styleSyncProdId)
        {
            var styleSync = _styleSyncContext.StyleSyncProds
                .SingleOrDefault(i => i.StyleSyncProdId == styleSyncProdId && i.IsActive);

            if (styleSync == null)
            {
                throw new Exception("The photo to be deleted was not found.");
            }
            
			styleSync.IsActive = false;

            try
            {
                await _styleSyncContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete StyleSyncProd: {ex.Message}");
            }

        }

        public async Task<List<StyleSyncProd>> GetList(int userId)
        {
             var list = await _styleSyncContext.StyleSyncProds.Where(ci => ci.IsActive && ci.UserId == userId).AsNoTracking().ToListAsync();
             if(!list.Any())
             {
                throw new Exception("No photos found to list.");
             }
             
             return list;
        }
    }
}