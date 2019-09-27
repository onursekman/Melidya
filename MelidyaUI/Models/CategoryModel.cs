using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelidyaUI.Models
{
    public class CategoryModel :BaseModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string CategoryDetail { get; set; }
    }
}
