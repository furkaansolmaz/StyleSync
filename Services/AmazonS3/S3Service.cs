 using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyncStyle.Services.AmazonS3
{
    public class S3Service : IS3Service
    {
        private readonly string _bucketName = "your-bucket-name";
        private readonly IAmazonS3 _s3Client;

        public S3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadImageToS3Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be empty.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, PNG, and WEBP are allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            try
            {
                await _s3Client.PutObjectAsync(request);
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload image to S3: {ex.Message}");
            }
        }
    }
}
