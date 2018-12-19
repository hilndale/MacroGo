﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class DailyFoodItemList
    {
        public List<FoodList> Items { get; set; } = new List<FoodList>();
        public UserProfileViewModel User { get; set; } //added by hs for adding goals to daily food list


        public void AddToList(FoodList food)
        {
            if (!Items.Contains(food))
            {
                Items.Add(food);
            }
        }

        public void RemoveFromList(FoodList food)
        {
            food = Items.FirstOrDefault();

            if (food != null && Items.Contains(food))
            {
                Items.Remove(food);
            }
        }
   
        
        ///// <summary>
        ///// Get the subtotal of all macros in the daily food list.
        ///// </summary>
        //public double SubTotal
        //{
        //    get
        //    {
        //        return Items.Sum(p => p.Price);
        //    }
        //}
    }
}

