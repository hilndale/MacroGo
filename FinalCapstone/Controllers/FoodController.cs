using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FinalCapstone.Controllers
{
    public class FoodController : Controller
    {
        public IActionResult FoodMaintenance()
        {
            return View();
        }
    }
}