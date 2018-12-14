using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Extensions;
using FinalCapstone.Models;
using FinalCapstone.Controllers;
using FinalCapstone.Dal;

namespace FinalCapstone.Models
{
    public class Navigation
    {
        private static UserSqlDAL userDAL = new UserSqlDAL("Data Source=.\\sqlexpress;Initial Catalog=MacroGo;Integrated Security=True");
        private UsersController controller = new UsersController(userDAL);

        public string Nav()
        {
            return controller.Nav();
        }
    }
}
