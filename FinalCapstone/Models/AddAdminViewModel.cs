using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class AddAdminViewModel
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
