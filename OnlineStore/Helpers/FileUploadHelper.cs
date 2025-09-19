namespace OnlineStore.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public static class FileUploadHelper
{
    public static async Task<string> UploadFileAsync(IFormFile file, string uploadFolderPath) // when dealing with files it must be async
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No Choosen File");

        // check allowed extention
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" }; // array
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!Array.Exists(allowedExtensions, e => e == ext))
            throw new ArgumentException("Not Supported file type");

            // create a unique file name
        var uniqueFileName = $"{Guid.NewGuid()}{ext}";

        // check if directory is there or create it 
        if (!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);

        var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName; // return name of file to store it into database
    }
}
