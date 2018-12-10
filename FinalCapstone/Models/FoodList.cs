using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class FoodList
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int RestaurantId {get; set;}
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
    }
}
