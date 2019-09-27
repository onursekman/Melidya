
using System;
using System.Collections.Generic;
using System.Text;

namespace MelidyaEntity.DataBase
{
   public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; }
        public int PageNumber { get; set; }
    }   
}
