using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

        IndexViewModel criteria = new IndexViewModel()
        {
            MinCarbs = 20,
            MaxCarbs = 40,
            MinProtein = 0,
            MaxProtein = 0,
            MinFat = 0,
            MaxFat = 20,
            RestaurantChosen = "Test"
        };

        [TestMethod]
        public void CarbTest()
        {
            bool result = indexModel.MeetsCarbRequirement(item.Carbs, criteria);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ProteinTest()
        {
            bool result = indexModel.MeetsProteinRequirement(item.Protein, criteria);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void FatTest()
        {
            bool result = indexModel.MeetsFatRequirement(item.Fat, criteria);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void RestaurantTest()
        {
            bool result = indexModel.MeetsRestaurantRequirement(item.RestaurantName, criteria);
            Assert.AreEqual(false, result);
        }
        
        [TestMethod]
        public void BlankRestaurantValueTest()
        {
            IndexViewModel criteriaNew = new IndexViewModel()
            {
                RestaurantChosen = ""
            };

            bool result = indexModel.MeetsRestaurantRequirement(item.RestaurantName, criteriaNew);
            Assert.AreEqual(true, result);
        }
    }
}
