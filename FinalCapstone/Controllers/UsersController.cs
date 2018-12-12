using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserDAL _userDAL;

        public UsersController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}