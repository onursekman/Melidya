using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Melidya.Admin.Panel.Models
{
    public class UserModel : BaseModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Password { get; set; }
     
    }
}
