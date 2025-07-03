using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Apropio.API.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient? _blobServiceClient;
        private readonly string? _localStoragePath;
        private readonly bool _useCloudStorage;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _useCloudStorage = !string.IsNullOrEmpty(_configuration.GetConnectionString("AzureBlobStorage"));
            
            if (_useCloudStorage)
            {
                _blobServiceClient = new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage"));
            }
            else
            {
                _localStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(_localStoragePath);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "imagenes")
        {
            if (!IsValidImageFile(file))
                throw new ArgumentException("Archivo de imagen inválido");

            var fileName = GenerateUniqueFileName(file.FileName);
            
            if (_useCloudStorage)
            {
                return await UploadToCloudAsync(file, fileName, folder);
            }
            else
            {
                return await UploadToLocalAsync(file, fileName, folder);
            }
        }

        public async Task<List<string>> UploadMultipleImagesAsync(List<IFormFile> files, string folder = "imagenes")
        {
            var uploadTasks = files.Select(file => UploadImageAsync(file, folder));
            var results = await Task.WhenAll(uploadTasks);
            return results.ToList();
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                if (_useCloudStorage)
                {
                    return await DeleteFromCloudAsync(imageUrl);
                }
                else
                {
                    return await DeleteFromLocalAsync(imageUrl);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            // Verificar extensión
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
                return false;

            // Verificar tipo MIME
            var allowedMimeTypes = new[] 
            { 
                "image/jpeg", "image/jpg", "image/png", "image/gif", 
                "image/bmp", "image/webp" 
            };
            
            if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                return false;

            // Verificar tamaño (máximo 10MB)
            const int maxSizeInBytes = 10 * 1024 * 1024;
            if (file.Length > maxSizeInBytes)
                return false;

            return true;
        }

        public string GetImageUrl(string fileName, string folder = "imagenes")
        {
            if (_useCloudStorage)
            {
                var containerName = _configuration["AzureBlobStorage:ContainerName"] ?? "imagenes";
                return $"https://{GetStorageAccountName()}.blob.core.windows.net/{containerName}/{folder}/{fileName}";
            }
            else
            {
                var baseUrl = _configuration["BaseUrl"] ?? "http://localhost:5000";
                return $"{baseUrl}/uploads/{folder}/{fileName}";
            }
        }

        private async Task<string> UploadToCloudAsync(IFormFile file, string fileName, string folder)
        {
            var containerName = _configuration["AzureBlobStorage:ContainerName"] ?? "imagenes";
            var containerClient = _blobServiceClient!.GetBlobContainerClient(containerName);
            
            // Crear el contenedor si no existe
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            
            var blobName = $"{folder}/{fileName}";
            var blobClient = containerClient.GetBlobClient(blobName);
            
            var headers = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };
            
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);
            
            return blobClient.Uri.ToString();
        }

        private async Task<string> UploadToLocalAsync(IFormFile file, string fileName, string folder)
        {
            var folderPath = Path.Combine(_localStoragePath, folder);
            Directory.CreateDirectory(folderPath);
            
            var filePath = Path.Combine(folderPath, fileName);
            
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            
            return GetImageUrl(fileName, folder);
        }

        private async Task<bool> DeleteFromCloudAsync(string imageUrl)
        {
            try
            {
                var uri = new Uri(imageUrl);
                var containerName = _configuration["AzureBlobStorage:ContainerName"] ?? "imagenes";
                var blobName = uri.Segments.Skip(2).Aggregate((a, b) => a + b).TrimStart('/');
                
                var containerClient = _blobServiceClient!.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);
                
                var response = await blobClient.DeleteIfExistsAsync();
                return response.Value;
            }
            catch
            {
                return false;
            }
        }

        private Task<bool> DeleteFromLocalAsync(string imageUrl)
        {
            try
            {
                var uri = new Uri(imageUrl);
                var relativePath = uri.AbsolutePath.TrimStart('/');
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Task.FromResult(true);
                }
                
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private string GenerateUniqueFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var uniqueId = Guid.NewGuid().ToString();
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            return $"{timestamp}_{uniqueId}{extension}";
        }

        private string GetStorageAccountName()
        {
            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            if (string.IsNullOrEmpty(connectionString))
                return string.Empty;

            var accountNameKey = "AccountName=";
            var startIndex = connectionString.IndexOf(accountNameKey) + accountNameKey.Length;
            var endIndex = connectionString.IndexOf(';', startIndex);
            
            return endIndex == -1 
                ? connectionString[startIndex..] 
                : connectionString[startIndex..endIndex];
        }
    }
} 