namespace Apropio.API.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string folder = "imagenes");
        Task<bool> DeleteImageAsync(string imageUrl);
        Task<List<string>> UploadMultipleImagesAsync(List<IFormFile> files, string folder = "imagenes");
        bool IsValidImageFile(IFormFile file);
        string GetImageUrl(string fileName, string folder = "imagenes");
    }
} 