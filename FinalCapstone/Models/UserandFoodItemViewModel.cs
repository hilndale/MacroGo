using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class UserandFoodItemViewModel
    {
        //public Users user { get; set; }
        //public FoodItemViewModel foodItem { get; set; }

        public int UserId { get; set; }
        public string Email { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int RestaurantId { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
        public string RestaurantChosen { get; set; }

    }
}
