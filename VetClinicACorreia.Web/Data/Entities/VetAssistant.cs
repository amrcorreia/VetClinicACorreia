using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Data.Entities
{
    public class VetAssistant : IEntity
    {

        public int Id { get; set; }


        //[MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characters lenght")]
        //[Required]
        //public string Name { get; set; }


        //[Display(Name = "Image")]
        //public string ImageUrl { get; set; }

        
        //public string TIN { get; set; }


        //public string Mobile { get; set; }


        //public string Email { get; set; }


        //[Display(Name = "Working Schedule")]
        //public string WorkingSchedule { get; set; }


        //[Display(Name = "Is Available ?")]
        //public bool IsAvailable { get; set; }


        //public string Remarks { get; set; }


        public User User { get; set; }


        //public string ImageFullPath
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(this.ImageUrl))
        //        {
        //            return null;
        //        }

        //        return $"https://vetclinicacorreia.azurewebsites.net{this.ImageUrl.Substring(1)}";
        //    }
        //}
    }
}
