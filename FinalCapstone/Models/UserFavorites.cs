using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class UserFavorites
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int FoodItemId { get; set; }
    }
}
