using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        /// <summary>
        /// upload image to defined folder
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";

            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\images\\{folder}",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/{folder}/{file}";
        }
    }
}
