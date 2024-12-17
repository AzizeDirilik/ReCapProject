using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper.Constants
{
    public class FilePath
    {
        public static string Full(string path, string root = FileType.root, string fileType = FileType.images)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), root + fileType, path);
        }
    }
}




