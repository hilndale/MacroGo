using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    //holds the information about a food item as a whole - food & restaurant info that will be displayed in results
    public class Item
    {
        public string FoodName { get; set; }
        public string RestaurantName { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
    }
}
