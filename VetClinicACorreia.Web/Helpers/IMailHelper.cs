using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinicACorreia.Web.Helpers
{
    public interface IMailHelper
    {
        /// <summary>
        /// send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendMail(string to, string subject, string body);
    }
}
