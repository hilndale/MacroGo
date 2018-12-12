using FinalCapstone.Dal;
using FinalCapstone.Extensions;
using System.Web;
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
    }
}