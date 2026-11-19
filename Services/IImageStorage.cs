using FormsApp.Models;

namespace FormsApp.Services
{
    public interface IImageStorage
    {
        Task<ImageUploadResult> SaveAsync(IFormFile imageFile);
    }
}
