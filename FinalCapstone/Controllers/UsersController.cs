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
                HttpContext.Session.Set(user.Email, SessionKeys.Username);
            }

            return RedirectToAction("Index", "Home");
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

            Users user = _userDAL.GetUser(model.EmailAddress);
            // Check to see if the username already exists
            if (user != null)
            {
                ModelState.AddModelError("username-exists", "That email address is not available");
                return View("Register", model);
            }
            else
            {
                // Convert from the ViewModel to a Data Model and Savae
                user = new Users()
                {
                    Email = model.EmailAddress,
                    Password = model.Password
                };
                _userDAL.SaveUser(user);

                //FormsAuthentication.SetAuthCookie(user.Email, true);
                HttpContext.Session.Set(model.EmailAddress, SessionKeys.Username);
            }

            return RedirectToAction("Users", "UserProfile");
        }

        public ActionResult UserProfile()
        {
            return View("UserProfile");
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