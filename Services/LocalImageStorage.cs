using FormsApp.Models;

namespace FormsApp.Services
{
    public class LocalImageStorage : IImageStorage
    {
        private readonly IWebHostEnvironment _environment;
        
        public LocalImageStorage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<ImageUploadResult> SaveAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return new ImageUploadResult
                {
                    IsSuccess = false,
                    FileName = null,
                    ErrorMessage = "Lütfen bir resim dosyası seçin"
                };
            }

            if (imageFile.Length > 2 * 1024 * 1024)
            {
                return new ImageUploadResult
                {
                    IsSuccess = false,
                    FileName = null,
                    ErrorMessage = "Dosya boyutu 2 MB'den fazla olamaz"
                };
            }

            var extension = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png"};

            if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return new ImageUploadResult
                {
                    IsSuccess = false,
                    FileName = null,
                    ErrorMessage = "Geçerli bir resim dosyası seçin (.jpg, .jpeg, .png)"
                };
            }

            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
            var directory = Path.Combine(_environment.WebRootPath, "images");

            var path = Path.Combine(directory, randomFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return new ImageUploadResult
            {
                IsSuccess = true,
                FileName = randomFileName,
                ErrorMessage = null
            };
        }
    }
}
