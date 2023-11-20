using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Book_Store.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private Account account;
        public CloudinaryImageRepository(IConfiguration config) {
            _configuration = config;
            account = new Account(
                Environment.GetEnvironmentVariable("Cloudinary_CloudName"),
                Environment.GetEnvironmentVariable("Cloudinary_API-Key"),
                Environment.GetEnvironmentVariable("Cloudinary_API-Secret")
                );
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);
            
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                return uploadResult.SecureUri.ToString();
            return null;
        }
    }
}
