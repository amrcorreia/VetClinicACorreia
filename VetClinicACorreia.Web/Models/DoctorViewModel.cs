using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using VetClinicACorreia.Web.Data.Entities;

namespace VetClinicACorreia.Web.Models
{
    public class DoctorViewModel : Doctor
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
