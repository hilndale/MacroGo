using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int GoalFat { get; set; }
        public int GoalCarbs { get; set; }
        public int GoalProtein { get; set; }
    }
}
