using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;


namespace FinalCapstone.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserDAL _userDAL;

        public UsersController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public IActionResult Navigation()
        {
            if (HttpContext.Session.Get(SessionKeys.Username) == null)
            {
                return PartialView("_AnonymousNav");
            }
            else
            {
                return PartialView("_AuthenticatedNav");
            }
        }

        // GET: User/Index
        public ActionResult UserIndex()
        {
            if (HttpContext.Session.Get(SessionKeys.Username) == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("UserIndex", "Users");
            }
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
                //FormsAuthentication.SetAuthCookie(user.Email, true);

                HttpContext.Session.Set(SessionKeys.Username, user.Email);
                viewModel = _userDAL.GetUserProfile(model.Email);
            }

            return RedirectToAction("UserProfile", viewModel);
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

                //FormsAuthentication.SetAuthCookie(user.Email, true);
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

            _userDAL.ChangePassword(HttpContext.Session.GetString(SessionKeys.Username), model.NewPassword);

            return RedirectToAction("UserProfile", "Users");
        }

            // POST: User/Logout
            public ActionResult Logout()
        {
            //    FormsAuthentication.SignOut();
            HttpContext.Session.Remove(SessionKeys.Username);
            return RedirectToAction("Index", "Home");
        }
    }
}