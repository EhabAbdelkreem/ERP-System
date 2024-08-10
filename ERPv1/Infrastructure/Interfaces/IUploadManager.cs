using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces
{
    public interface IUploadManager
    {
        string UploadedFile(IFormFile Pic, string FolderName);
    }
}