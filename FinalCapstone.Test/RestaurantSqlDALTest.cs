using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalCapstone.Dal;

namespace FinalCapstone.Test
{
    [TestClass]
    public class RestaurantSqlDALTest : DatabaseTest
    {
        private IRestaurantDAL _restaurantDAL;

        [TestInitialize]
        public void Setup()
        {
            _restaurantDAL = new RestaurantSqlDAL(MacroGoConnectionString);
        }

        [TestClass]
        public class RestaurantTest : RestaurantSqlDALTest
        {
            [TestMethod]
            public void No_restaurants_exist()
            {
                //sample test method to verify that the DatabaseTest setup is correct and there is no data in the table for test
                //var restaurants = _restaurantDAL.GetRestaurants();
                //Assert.IsFalse(restaurants.Any());
            }

        }
    }
}
