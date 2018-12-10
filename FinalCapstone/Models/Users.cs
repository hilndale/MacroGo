using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public int MaxProtein { get; set; }
        public int MaxFat { get; set; }
        public int MaxCarbs { get; set; }
        public int MaxCalories { get; set; }
    }
}
