using Microsoft.AspNetCore.Mvc.Rendering;
using FinalCapstone.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class FoodItemViewModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int RestaurantId { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
        public string RestaurantChosen { get; set; }

        public IList<SelectListItem> RestaurantSelect { get; set; }
       

    }

    
}
