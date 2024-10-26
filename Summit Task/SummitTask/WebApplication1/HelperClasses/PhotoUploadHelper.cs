namespace Summit_Task.HelperClasses
{
    public class PhotoUploadHelper
    {
        public static async Task<string> SavePhotoAsync(IFormFile photo, string uploadFolderPath)
        {
            if (photo == null || photo.Length == 0)
            {
                throw new ArgumentException("No file was uploaded.");
            }

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Only image files are allowed (jpg, jpeg, png, gif).");
            }

            // Generate a Unique filename
            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadFolderPath, uniqueFileName);
            if (!Directory.Exists(Path.Combine(uploadFolderPath)))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
    }
}
