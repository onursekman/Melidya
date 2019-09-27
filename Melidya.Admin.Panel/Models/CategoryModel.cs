using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Melidya.Admin.Panel.Models
{
    public class CategoryModel:BaseModel
    {
        public int CategoryID { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public List<SelectListItem> CategorySelectList { get; set; }
    }
}
