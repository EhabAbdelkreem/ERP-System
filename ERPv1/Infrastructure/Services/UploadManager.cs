using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UploadManager : IUploadManager
    {
        private readonly IWebHostEnvironment _env;

        public UploadManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string UploadedFile(IFormFile Pic, string FolderName)
        {
            string uniqueFileName = null;

            if (Pic != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, FolderName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Pic.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Pic.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
