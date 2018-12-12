﻿using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using Newtonsoft.Json;

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

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Search));
        }

        public IActionResult Search()
        {
            IndexViewModel model = new IndexViewModel();

            IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
            IList<SelectListItem> RestaurantSelections = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "All Restaurants"},
            };

            foreach (Restaurant restaurant in Restaurants)
            {
                RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantName });
            }

            model.RestaurantSelect = RestaurantSelections;

            return View(model);
        }

        public IActionResult Result(IndexViewModel model)
        {
            IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
            IList<SelectListItem> RestaurantSelections = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "All Restaurants"},
            };

            foreach (Restaurant restaurant in Restaurants)
            {
                RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantName });
            }

            model.RestaurantSelect = RestaurantSelections;

            IndexModel getResults = new IndexModel();

            List<Item> allItems = _foodDAL.GetAllFoodItems();

            model.Results = getResults.GetResult(allItems, model);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddFoodItem()
        {
            FoodItemViewModel foodItemViewModel = new FoodItemViewModel();

            IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
            IList<SelectListItem> RestaurantSelections = new List<SelectListItem>();

            foreach (Restaurant restaurant in Restaurants)
            {
                RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantId.ToString() });
            }

            foodItemViewModel.RestaurantSelect = RestaurantSelections;

            return View(foodItemViewModel);
        }

        [HttpGet]
        public IActionResult RestaurantDetail(int id)
        {
            Restaurant restaurant = _restaurantDAL.GetRestaurant(id);

            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodItem(FoodItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                FoodItemViewModel foodItemViewModel = new FoodItemViewModel();

                IList<Restaurant> Restaurants = _restaurantDAL.GetRestaurants();
                IList<SelectListItem> RestaurantSelections = new List<SelectListItem>();

                foreach (Restaurant restaurant in Restaurants)
                {
                    RestaurantSelections.Add(new SelectListItem() { Text = restaurant.RestaurantName, Value = restaurant.RestaurantId.ToString() });
                }

                foodItemViewModel.RestaurantSelect = RestaurantSelections;

                return View(foodItemViewModel);

            }

            else
            {

                FoodList food = new FoodList();
                food.FoodName = model.FoodName;
                food.RestaurantId = int.Parse(model.RestaurantChosen);
                food.Calories = model.Calories;
                food.Carbs = model.Carbs;
                food.Fat = model.Fat;
                food.Protein = model.Protein;

                _foodDAL.AddFoodItem(food);
                TempData["msg"] = "<button><strong> Your item has been added!</strong></button>";
                return RedirectToAction(nameof(AddFoodItem));
            }
        }

        [HttpGet]
        public IActionResult DeleteFoodItem()
        {
            FoodItemViewModel model = new FoodItemViewModel();
            return View("DeleteFoodItem", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFoodItem(FoodList model)
        {
            FoodList food = _foodDAL.GetFood(model.FoodId);
            //checked to see if user is admin?
            if (food == null)
            {
                return View("DeleteFoodItem", model);
            }

            _foodDAL.DeleteFoodItem(model);
            TempData["msg"] = "Your item has been deleted!"; //need session?
            return RedirectToAction(nameof(DeleteFoodItem));
        }

        public IActionResult ChangeFoodItem(FoodItemViewModel model)
        {
            //this is shell only

            return View(model);
        }


        [HttpGet]
        public IActionResult FoodDetail(int id)
        {
            FoodList food = _foodDAL.GetFood(id);
            FoodItemViewModel foodModel = new FoodItemViewModel();

            foodModel.FoodId = food.FoodId;
            foodModel.FoodName = food.FoodName;
            foodModel.RestaurantId = food.RestaurantId;
            foodModel.Protein = food.Protein;
            foodModel.Fat = food.Fat;
            foodModel.Carbs = food.Carbs;
            foodModel.Calories = food.Calories;

            //Restaurant restaurant = _restaurantDAL.GetRestaurant(food.RestaurantId);
            //foodModel.RestaurantChosen;

            return View(foodModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFoodItem(FoodList model)
        {

            FoodList food = _foodDAL.GetFood(model.FoodId);
            //checked to see if user is admin?
            if (food == null)
            {
                return View("DeleteFoodItem", model);
            }

            _foodDAL.UpdateFoodItem(model);
            TempData["msg"] = "Your item has been changed!"; //need session?
            return RedirectToAction(nameof(DeleteFoodItem));

        }

    }


}
// will provide routes to welcome page and index