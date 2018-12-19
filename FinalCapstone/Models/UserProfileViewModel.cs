using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }
        public int IsAdmin { get; set; }
        public int UserId { get; set; } //added by hilary 

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int GoalFat { get; set; }
        public int GoalProtein { get; set; }
        public int GoalCarbs { get; set; }
    }
}
