using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FinalCapstone.Controllers;
using FinalCapstone.Dal;

namespace FinalCapstone.Test
{
    [TestClass]
    public class IndexTest
    {
        IndexModel indexModel = new IndexModel();

        Item item = new Item()
        {
            FoodName = "Burger",
            RestaurantName = "Test Restaurant",
            Protein = 50,
            Fat = 20,
            Carbs = 60,
            Calories = 400
        };

        IndexViewModel criteriaPass = new IndexViewModel()
        {
            MinCarbs = 20,
            MaxCarbs = 100,
            MinProtein = 40,
            MaxProtein = 0,
            MinFat = 0,
            MaxFat = 20,
            RestaurantChosen = "All Restaurants"
        };

        IndexViewModel criteriaFail = new IndexViewModel()
        {
            MinCarbs = 20,
            MaxCarbs = 40,
            MinProtein = 20,
            MaxProtein = 30,
            MinFat = 0,
            MaxFat = 10,
            RestaurantChosen = "Test"
        };

        [TestMethod]
        public void CarbFailTest()
        {
            bool result = indexModel.MeetsCarbRequirement(item.Carbs, criteriaFail);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CarbPassTest()
        {
            bool result = indexModel.MeetsCarbRequirement(item.Carbs, criteriaPass);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ProteinPassTest()
        {
            bool result = indexModel.MeetsProteinRequirement(item.Protein, criteriaPass);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ProteinFailTest()
        {
            bool result = indexModel.MeetsProteinRequirement(item.Protein, criteriaFail);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void FatPassTest()
        {
            bool result = indexModel.MeetsFatRequirement(item.Fat, criteriaPass);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void FatFailTest()
        {
            bool result = indexModel.MeetsFatRequirement(item.Fat, criteriaFail);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void RestaurantTest()
        {
            bool result = indexModel.MeetsRestaurantRequirement(item.RestaurantName, criteriaFail);
            Assert.AreEqual(false, result);
        }
        
        [TestMethod]
        public void AllRestaurantValueTest()
        {
            bool result = indexModel.MeetsRestaurantRequirement(item.RestaurantName, criteriaPass);
            Assert.AreEqual(true, result);
        }
    }
}
