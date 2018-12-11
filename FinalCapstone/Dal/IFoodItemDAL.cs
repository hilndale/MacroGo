using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public interface IFoodItemDAL
    {
        bool AddFoodItem(FoodList food);
        bool DeleteFoodItem(FoodList food);
        bool UpdateFoodItem(FoodList food);
        FoodList GetFood(int foodId); // added by HS, trying to get delete post to work
        List<Item> GetAllFoodItems();
    }
}
