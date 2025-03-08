namespace SyncStyle.Services.AmazonS3
{
    public interface IS3Service
    {
        Task<string> UploadImageToS3Async(IFormFile file);
    }
}