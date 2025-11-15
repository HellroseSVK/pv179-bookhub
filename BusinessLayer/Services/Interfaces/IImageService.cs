using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services.Interfaces;

public interface IImageService
{
    Task<string> SaveBookImageAsync(IFormFile imageFile, int bookId);
    Task DeleteBookImageAsync(string imagePath);
    bool IsValidImageFile(IFormFile file);
}