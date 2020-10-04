using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Helpers
{
    public interface IImageHelper
    {
        /// <summary>
        /// upload image to defined folder
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        Task<String> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
