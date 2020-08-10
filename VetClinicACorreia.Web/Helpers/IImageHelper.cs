﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Helpers
{
    public interface IImageHelper
    {
        Task<String> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
