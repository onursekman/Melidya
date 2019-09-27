using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Melidya.Admin.Panel.Models
{
    public class BaseModel
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public bool? Success { get; set; }
    }
}
