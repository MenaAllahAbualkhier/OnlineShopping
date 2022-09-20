using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Demo.BL.Helper
{
    public static class UploadFile
    {
       
        public static string uploadFile(IFormFile file, string path)
        {
            try
            {
                var ImagePath = Directory.GetCurrentDirectory() + path;
                var ImageName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                var ImageFinalPath = Path.Combine(ImagePath, ImageName);
                using (var streem = new FileStream(ImageFinalPath, FileMode.Create))
                {
                    file.CopyTo(streem);
                }
                return ImageName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public static string DeleteFile(string fileName, string path)
        {
            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + path + fileName))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + path + fileName);
                }
                return "deleted";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
