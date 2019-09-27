using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MelidyaUI.Models
{
    public class MessageModell : BaseModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string MessageTitle { get; set; }
        [Required]
        public string MessageDetail { get; set; }
        [Required]
        public string Email { get; set; }

        public DateTime Date { get; set; }
    }
}
