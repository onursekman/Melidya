using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelidyaUI.Models
{
    public class ProductModel : BaseModel
    {
        public int ID { get; set; }
        public string Product_Name { get; set; }
        public string Product_Detail { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public int PageNumber { get; set; }
    }
}
