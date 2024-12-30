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

        public void Delete(string filePath)
        {
            var fullPath = FilePath.Full(filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Console.WriteLine($"File deleted: {fullPath}");
            }
            else
            {
                Console.WriteLine($"File not found: {fullPath}");
            }


        }


        public void Update(IFormFile file, string imagePath)
        {
            //bu kısımda var olan guid'i silip eklemek yerine
            //guid yapısı aynı sadece dosyayı değiştiriyorum.
            var fullpath = FilePath.Full(imagePath);
            if (Path.Exists(fullpath))
            {
                using FileStream fileStream = new(fullpath, FileMode.Create);
                //FileMode.Create burada üzerine yazma işlemi yapar.
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            else
            {
                throw new DirectoryNotFoundException("Hata");
            }

        }
    }
}
    
