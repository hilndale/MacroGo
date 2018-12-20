using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FinalCapstone.Controllers
{
    public class FoodController : UsersController
    {
        private readonly IFoodItemDAL _foodDAL;
        private readonly IRestaurantDAL _restaurantDAL;
        private readonly IUserDAL _userDAL;
        private readonly IUserFavoritesSqlDAL _userfavoritesDAL;

        public FoodController(IFoodItemDAL foodDAL, IRestaurantDAL restaurantDAL, IUserDAL userDAL, IUserFavoritesSqlDAL userFavoritesDAL) : base(userDAL, userFavoritesDAL)
        {
            _foodDAL = foodDAL;
            _restaurantDAL = restaurantDAL;
            _userDAL = userDAL;
            _userfavoritesDAL = userFavoritesDAL;
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
                return RedirectToAction(nameof(AddFoodItem));
            }
        }

        [HttpGet]
        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRestaurant(Restaurant model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            else
            {
                _restaurantDAL.AddRestaurant(model);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFoodItem(FoodItemViewModel model)
        {
            FoodList food = new FoodList();

            food.FoodId = model.FoodId;
            food.FoodName = model.FoodName;
            food.RestaurantId = model.RestaurantId;
            food.Protein = model.Protein;
            food.Fat = model.Fat;
            food.Carbs = model.Carbs;
            food.Calories = model.Calories;

            _foodDAL.DeleteFoodItem(food);

            return RedirectToAction("Index", "Home");
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

            return View("FoodDetail", foodModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FoodDetail(FoodItemViewModel model)
        {
            FoodList food = new FoodList();

            food.FoodId = model.FoodId;
            food.FoodName = model.FoodName;
            food.RestaurantId = model.RestaurantId;
            food.Protein = model.Protein;
            food.Fat = model.Fat;
            food.Carbs = model.Carbs;
            food.Calories = model.Calories;

            _foodDAL.UpdateFoodItem(food);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFoodItemToList(FoodItemViewModel model)
        {
            FoodList food = new FoodList();
            DailyFoodItemList listItems = GetActiveDailyFoodItemList();

            food.FoodId = model.FoodId;
            food.FoodName = model.FoodName;
            food.Protein = model.Protein;
            food.Carbs = model.Carbs;
            food.Fat = model.Fat;

            listItems.AddToList(food);
            SetActiveDailyFoodItemList(listItems);

            return RedirectToAction("ViewDailyFoodItemList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFoodItemFromList(FoodItemViewModel model)
        {
            FoodList food = new FoodList();
            DailyFoodItemList listItems = GetActiveDailyFoodItemList();

            food.FoodId = model.FoodId;
            food.FoodName = model.FoodName;
            food.Protein = model.Protein;
            food.Carbs = model.Carbs;
            food.Fat = model.Fat;

            listItems.RemoveFromList(food);
            SetActiveDailyFoodItemList(listItems);

            return RedirectToAction("ViewDailyFoodItemList");

        }
        [HttpGet]
        public IActionResult DisplayFoodItems(int userId)
        {
            userId = GetActiveUserFromSession();
            if (userId == 0)
            {
                DailyFoodItemList listItemsAnon = GetActiveDailyFoodItemList();
                return RedirectToAction("ViewDailyFoodItemList");
            }
            UserProfileViewModel model = _userDAL.GetUserProfile(HttpContext.Session.GetString(SessionKeys.Username));

            DailyFoodItemList listItems = GetActiveDailyFoodItemList();
            listItems.User.GoalCarbs = model.GoalCarbs;
            listItems.User.GoalFat = model.GoalFat;
            listItems.User.GoalProtein = model.GoalProtein;
            return RedirectToAction("ViewDailyFoodItemList", model);
        }

        // GET: ViewDailyFoodItemList
        public ActionResult ViewDailyFoodItemList(int userId)
        {
            userId = GetActiveUserFromSession();
            if (userId == 0)
            {
                DailyFoodItemList listItemsAnon = GetActiveDailyFoodItemList();
                return View("DailyFoodItemList", listItemsAnon);
            }

            UserProfileViewModel model = _userDAL.GetUserProfile(HttpContext.Session.GetString(SessionKeys.Username));
            DailyFoodItemList foodList = GetActiveDailyFoodItemList();
            foodList.User.GoalCarbs = model.GoalCarbs;
            foodList.User.GoalFat = model.GoalFat;
            foodList.User.GoalProtein = model.GoalProtein;
            return View("DailyFoodItemList", foodList);
        }

        // Returns the active daily food item list. If there isn't one, then one is created.
        public DailyFoodItemList GetActiveDailyFoodItemList() //was private
        {
            if (HttpContext.Session.Get(SessionKeys.DailyList) == null)
            {
                HttpContext.Session.Set(SessionKeys.DailyList, new DailyFoodItemList());
            }
            return HttpContext.Session.Get<DailyFoodItemList>(SessionKeys.DailyList);
        }

        // Returns the active daily food item list. If there isn't one, then one is created.
        private void SetActiveDailyFoodItemList(DailyFoodItemList listItems)
        {
            HttpContext.Session.Set(SessionKeys.DailyList, listItems);
        }
    }
}