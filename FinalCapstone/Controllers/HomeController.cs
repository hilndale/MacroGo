using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using FinalCapstone.Extensions;

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

        [HttpGet]
        public IActionResult Index()
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

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            IndexModel getResults = new IndexModel();
            ResultViewModel viewModel = new ResultViewModel();

            List<Item> allItems = _foodDAL.GetAllFoodItems();

            viewModel.Results = getResults.GetResult(allItems, model);
            TempData["viewModel"] = viewModel;
            TempData.Put("key", viewModel);

            return RedirectToAction(nameof(Result));
        }

        //[HttpPost] 
        //public IActionResult Index(IndexViewModel model)
        //{
        //    IndexModel getResults = new IndexModel();

        //    List<Item> allItems = _foodDAL.GetAllFoodItems();

        //    ResultViewModel viewModel = new ResultViewModel();
        //    viewModel.Results = getResults.GetResult(allItems, model);

        //    return RedirectToAction(nameof(Result));
        //}

        [HttpGet]
        public IActionResult Result()
        {
            ResultViewModel viewModel = (ResultViewModel) TempData.Get<ResultViewModel>("key");

            return View(viewModel);
        }

        //[HttpGet]
        //public IActionResult Result(IndexViewModel model)
        //{
        //    IndexModel getResults = new IndexModel();
        //    ResultViewModel viewModel = new ResultViewModel();

        //    List<Item> allItems = _foodDAL.GetAllFoodItems();

        //    viewModel.Results = getResults.GetResult(allItems, model);
        //    return View(viewModel);
        //}

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodItem(FoodList model)
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
                // need to move FoodItemViewModel fields to FoodList fields - note that we must retrieve the value from the restaurant selectlistitem
                _foodDAL.AddFoodItem(model);
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
        public IActionResult UpdateFoodItem()
        {
            FoodItemViewModel updatedFood = new FoodItemViewModel();
            return View(updatedFood);
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