

using System;
using System.Collections.Generic;
using System.Text;

namespace MelidyaEntity.DataBase
{
   public class User 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

    }
}
