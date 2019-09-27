using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melidya.Admin.Panel.Models
{
    public class HomeModel : BaseModel
    {
        public List<ProductModel> productModels  { get; set; }
        public List<CategoryModel> categoryModels{ get; set; }
        public List<MessageModel> messageModels{ get; set; }

    }
}
