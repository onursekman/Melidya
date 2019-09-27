using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melidya.Admin.Panel.Models
{
    public class SenderMessageModel
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int MessageID { get; set; }
        public string Email { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }





    }
}
