using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;

namespace Core.Services;

public class FileService
{
    private readonly DriveService _driveService;
    private readonly string _folderId;

    public FileService(string serviceFilePath, string folderId)
    {
        _folderId = folderId;
        GoogleCredential credential;

        if (File.Exists(serviceFilePath))
        {
            credential = GoogleCredential.FromFile(serviceFilePath)
                .CreateScoped(DriveService.Scope.Drive);
        }
        else
        {
            var encodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SERVICE_CREDENTIAL");

            if (string.IsNullOrEmpty(encodedCredential))
            {
                throw new InvalidOperationException("Google service credential not found in environment variables.");
            }

            var jsonCredential = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredential));

            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonCredential));
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.Scope.Drive);
        }

        _driveService = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "GoogleDriveIntegration"
        });
    }

    public async Task<string> UploadFileAsync(byte[] fileContent, string fileName, string mimeType)
    {
        return await UploadFileInternalAsync(fileContent, fileName, mimeType);
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        var fileContent = await ConvertToByteArray(file);
        return await UploadFileInternalAsync(fileContent, fileName, file.ContentType);
    }

    private async Task<string> UploadFileInternalAsync(byte[] fileContent, string fileName, string mimeType)
    {
        var existingFile = await GetFile(fileName);
        if (existingFile != null)
        {
            await DeleteFileAsync(existingFile.Id);
        }

        var fileMetadata = new Google.Apis.Drive.v3.Data.File
        {
            Name = fileName,
            Parents = [_folderId]
        };

        FilesResource.CreateMediaUpload uploadRequest;
        using (var stream = new MemoryStream(fileContent))
        {
            uploadRequest = _driveService.Files.Create(fileMetadata, stream, mimeType);
            uploadRequest.Fields = "id, webViewLink";
            await uploadRequest.UploadAsync();
        }

        var uploadedFile = uploadRequest.ResponseBody;

        var permission = new Google.Apis.Drive.v3.Data.Permission
        {
            Role = "reader",
            Type = "anyone"
        };

        await _driveService.Permissions.Create(permission, uploadedFile.Id).ExecuteAsync();
        return $"https://drive.google.com/uc?export=download&id={uploadedFile.Id}";
    }

    public static async Task<byte[]> ConvertToByteArray(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<Google.Apis.Drive.v3.Data.File?> GetFile(string fileName)
    {
        var request = _driveService.Files.List();
        request.Q = $"name = '{fileName}' and '{_folderId}' in parents and trashed = false";
        request.Fields = "files(id)";

        var result = await request.ExecuteAsync();
        return result.Files.FirstOrDefault();
    }

    public async Task DeleteFileAsync(string fileId)
    {
        await _driveService.Files.Delete(fileId).ExecuteAsync();
    }
}