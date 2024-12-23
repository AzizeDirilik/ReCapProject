using Core.Utilities.Helpers.FileHelper.Constants;
using Core.Utilities.Helpers.GuildHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FIleHelper : IFileHelper
    {
        public string Add(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = GuidHelper.Create() + fileExtension;
            var imagePath = FilePath.Full(uniqueFileName);
            using FileStream fileStream = new(imagePath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Flush();
            return uniqueFileName;
        }

        public void Delete(string path)
        {
            if (Path.Exists(FilePath.Full(path)))
            {
                File.Delete(FilePath.Full(path));
            }
           
        }

        public void Update(IFormFile file, string imagePath)
        {
            var fullPath = FilePath.Full(imagePath);
            if (Path.Exists(fullPath))
            {
                using FileStream fileStream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            throw new DirectoryNotFoundException();
        }
    }
}
