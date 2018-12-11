using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalCapstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodItemDAL _foodDAL;
        private readonly IRestaurantDAL _restaurantDAL;

        public HomeController(IFoodItemDAL foodDAL, IRestaurantDAL restaurantDAL)
        {
            _foodDAL = foodDAL;
            _restaurantDAL = restaurantDAL;
        }

        public IActionResult Index(IndexViewModel model)
        {
            IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
            IList<SelectListItem> RestaurantSelections = new List<SelectListItem>();

            foreach (Restaurant restaurant in Restaurants)
            {
                RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantName });
            }

            model.RestaurantSelect = RestaurantSelections;

            return View(model);
        }

        //public IActionResult Result(IndexViewModel model)
        //{
        //    IndexModel getResults = new IndexModel();

        //    List<Item> allItems = _foodDAL.GetFoods();

        //    List<Item> resultItems = getResults.GetResult(allItems, model);


        //    return View(resultItems);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddFoodItem(FoodItemViewModel model)
        {
            IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
            IList<SelectListItem> RestaurantSelections = new List<SelectListItem>();

            foreach (Restaurant restaurant in Restaurants)
            {
                RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantName });
            }

            model.RestaurantSelect = RestaurantSelections;

            return View(model);
        }

        //public IActionResult UpdateFoodItem(FoodList model)
        //{
        //    bool updatedFood = _foodDAL.UpdateFoodItem(model);

        //    if (updatedFood == false)
        //    {
        //        return View(UpdateFoodItem)
        //    }
        //    return View(updatedFood);
        //}
    }


}
// will provide routes to welcome page and index