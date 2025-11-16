using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services;

public class ImageService : IImageService
{
    private readonly string _imageStoragePath;
    private readonly long _maxFileSize;
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];

    public ImageService(IConfiguration configuration)
    {
        _imageStoragePath = configuration["FileStorage:ImagePath"] ?? "wwwroot/images/books";
        _maxFileSize = configuration.GetValue<long>("FileStorage:MaxFileSize", 5 * 1024 * 1024); // 5MB default

        EnsureDirectoryExists(_imageStoragePath);
    }

    public async Task<string> SaveBookImageAsync(IFormFile imageFile, int bookId)
    {
        if (!IsValidImageFile(imageFile))
        {
            throw new ArgumentException("Invalid image file");
        }

        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        var fileName = $"book_{bookId}_{Guid.NewGuid():N}{fileExtension}";
        var filePath = Path.Combine(_imageStoragePath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        return fileName;
    }

    public async Task DeleteBookImageAsync(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
            return;

        var filePath = Path.Combine(_imageStoragePath, imagePath);

        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }

    public bool IsValidImageFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return false;

        if (file.Length > _maxFileSize)
            return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            return false;

        return IsValidImageContentType(file.ContentType);
    }

    private static bool IsValidImageContentType(string contentType)
    {
        var allowedContentTypes = new[]
        {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        return allowedContentTypes.Contains(contentType.ToLowerInvariant());
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}