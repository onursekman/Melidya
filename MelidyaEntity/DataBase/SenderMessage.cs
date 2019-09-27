using System;
using System.Collections.Generic;
using System.Text;

namespace MelidyaEntity.DataBase
{
    public class SenderMessage
    {

        public int ID { get; set; }
        public int SenderID { get; set; }
        public int MessageID { get; set; }
        public string Email { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }


    }
}
