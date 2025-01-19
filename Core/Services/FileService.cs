using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public class FileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public FileService(string connectionString, string containerName)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        public async Task<string> UploadFileAsync(byte[] fileContent, string fileName, string contentType)
        {
            return await UploadFileInternalAsync(fileContent, fileName, contentType);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            var fileContent = await ConvertToByteArray(file);
            return await UploadFileInternalAsync(fileContent, fileName, file.ContentType);
        }

        private async Task<string> UploadFileInternalAsync(byte[] fileContent, string fileName, string contentType)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            await using var stream = new MemoryStream(fileContent);
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri.ToString();
        }

        public static async Task<byte[]> ConvertToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            return await blobClient.ExistsAsync();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
