using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Melidya.Admin.Panel.Models
{
    public class ProductModel : BaseModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string ProductDetail { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public int CategoryID { get; set; }
        public string ImageUI { get; set; }
        public List<CategoryModel> categoryModel { get; set; }
        public int PageNumber { get; set; }

    }
}
