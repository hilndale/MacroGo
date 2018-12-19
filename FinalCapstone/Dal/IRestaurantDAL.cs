using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IRestaurantDAL
    {
        IList<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        bool AddRestaurant(Restaurant restaurant);
    }
}
