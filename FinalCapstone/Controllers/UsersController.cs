using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinalCapstone.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserDAL _userDAL;
        private readonly IUserFavoritesSqlDAL _userfavoritesDAL;

        public UsersController(IUserDAL userDAL, IUserFavoritesSqlDAL userFavoritesDAL)
        {
            _userDAL = userDAL;
            _userfavoritesDAL = userFavoritesDAL;
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            UserProfileViewModel viewModel = new UserProfileViewModel();

            Users user = _userDAL.GetUser(model.Email);
            // user does not exist or password is wrong
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Login", model);
            }
            else
            {
                HttpContext.Session.Set(SessionKeys.Username, user.Email);
                HttpContext.Session.Set(SessionKeys.AdminFlag, user.IsAdmin);

                if (_userDAL.IsAdmin(user.Email))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    viewModel = _userDAL.GetUserProfile(model.Email);
                    return RedirectToAction("UserProfile", viewModel);
                }
            }
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            AddAdminViewModel model = new AddAdminViewModel();
            return View("AddAdmin", model);
        }

        [HttpPost]
        public ActionResult AddAdmin(AddAdminViewModel model)
        {
            if (!_userDAL.IsAdmin(model.EmailAddress))
            {
                _userDAL.AddAdmin(model.EmailAddress);
                return RedirectToAction("Index", "Home");
            }

            return View("AddAdmin", model);
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            UserProfileViewModel viewModel = new UserProfileViewModel();
            Users user = _userDAL.GetUser(model.EmailAddress);

            // Check to see if the username already exists
            if (user != null)
            {
                ModelState.AddModelError("email-exists", "That email address is already associated with an account");
                return View("Register", model);
            }
            else
            {
                user = new Users()
                {
                    Email = model.EmailAddress,
                    Password = model.Password,
                    GoalCarbs = model.GoalCarbs,
                    GoalProtein = model.GoalProtein,
                    GoalFat = model.GoalFat
                };
                _userDAL.SaveUser(user);

                HttpContext.Session.Set(SessionKeys.Username, model.EmailAddress);
            }

            return RedirectToAction("UserProfile", "Users");
        }

        public ActionResult UserProfile(UserProfileViewModel viewModel)
        {
            viewModel = _userDAL.GetUserProfile(HttpContext.Session.GetString(SessionKeys.Username));

            return View("UserProfile", viewModel);
        }

        [HttpGet]
        public ActionResult UpdateUserGoals()
        {
            UserProfileViewModel viewModel = new UserProfileViewModel();
            return View("UpdateUserGoals", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateUserGoals(UserProfileViewModel viewModel)
        {
            viewModel.Email = HttpContext.Session.GetString(SessionKeys.Username);
            bool success = _userDAL.UpdateGoals(viewModel);

            return RedirectToAction("UserProfile", viewModel);
        }

        [HttpGet]
        public ActionResult Favorites()
        {
            UserFavoritesViewModel model = new UserFavoritesViewModel();
            return View("Favorites", model);
        }

        [HttpGet]
        public ActionResult Favorites_original()
        {
            UserFavoritesViewModel model = new UserFavoritesViewModel();
            return View("Favorites", model);
        }


        [HttpGet]
        public ActionResult Favorites(int userID)
        {
            userID = (int)HttpContext.Session.GetInt32(SessionKeys.UserId);
            UserFavoritesViewModel model = new UserFavoritesViewModel();
            model.Favorites = _userfavoritesDAL.GetFavorites(userID);

            return View("Favorites", model);
        }

        //[HttpGet]
        //public IActionResult FoodDetail(int id)
        //{
        //    FoodList food = _foodDAL.GetFood(id);
        //    FoodItemViewModel foodModel = new FoodItemViewModel();

        //    foodModel.FoodId = food.FoodId;
        //    foodModel.FoodName = food.FoodName;
        //    foodModel.RestaurantId = food.RestaurantId;
        //    foodModel.Protein = food.Protein;
        //    foodModel.Fat = food.Fat;
        //    foodModel.Carbs = food.Carbs;
        //    foodModel.Calories = food.Calories;

        //    return View(foodModel);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddFoodToFavorites(UserandFoodItemViewModel model)
        //{
        //    FoodList food = new FoodList();
        //    string userEmail = GetActiveUser();

        //    user.UserId = userEmail;
        //    food.FoodId = model.FoodId;
        //    food.RestaurantId = model.RestaurantId;
        //    //  want to redirect to favorites list
        //    return RedirectToAction("Index", "Home");
        //}


        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View("ChangePassword", model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ChangePassword", model);
            }

            Users user = _userDAL.GetUser(HttpContext.Session.GetString(SessionKeys.Username));

            if (model.OldPassword == user.Password)
            {
                _userDAL.ChangePassword(HttpContext.Session.GetString(SessionKeys.Username), model.NewPassword);
                return RedirectToAction("UserProfile", "Users");
            }
            else
            {
                model.ErrorMessage = "Old password is not correct";
                return View("ChangePassword", model);
            }
        }

        // POST: User/Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKeys.Username);
            HttpContext.Session.Remove(SessionKeys.AdminFlag);
            return RedirectToAction("Index", "Home");
        }
    }
}