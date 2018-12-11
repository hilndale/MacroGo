using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;
using FinalCapstone.Dal;

namespace FinalCapstone.Controllers
{
    public class ApiController : Controller
    {
        private readonly IFoodItemDAL _foodDAL;

        public ApiController(IFoodItemDAL foodDAL)
        {
            _foodDAL = foodDAL;
        }

        //public ActionResult GetResults(int minCarbs, int maxCarbs, int minProtein, int maxProtein, int minFat, int maxFat, string restaurant)
        //{
        //    List<Item> allItems = _foodDAL.GetAllFoodItems();
        //    IndexModel model = new IndexModel();

        //    IList<Item> Results = model.GetResult(allItems, minCarbs, maxCarbs, minProtein, maxProtein, minFat, maxFat, restaurant);

        //    return Json(Results);
        //}
    }
}