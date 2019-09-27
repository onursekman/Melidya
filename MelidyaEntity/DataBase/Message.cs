
using System;
using System.Collections.Generic;
using System.Text;

namespace MelidyaEntity.DataBase
{
    public class Message 
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }

    }
}
