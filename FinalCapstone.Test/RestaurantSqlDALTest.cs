using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalCapstone.Dal;
using System.Linq;
using System.Data.SqlClient;
using FinalCapstone.Models;

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
                var restaurants = _restaurantDAL.GetRestaurants();
                Assert.IsFalse(restaurants.Any());
            }

            [TestMethod]
            public void GetRestaurantsTest()
            {
                using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Restaurants (Restaurant_Name, Open_Time, Close_Time)" +
                        " VALUES ('Test Restaurant', '6:00am', '10:00pm')";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.ExecuteScalar();
                }

                var restaurants = _restaurantDAL.GetRestaurants();

                Assert.AreEqual(1, restaurants.Count);
            }

            [TestMethod]
            public void GetRestaurantTest()
            {
                int id;

                using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Restaurants (Restaurant_Name, Open_Time, Close_Time)" +
                        " VALUES ('Test Restaurant', '6:00am', '10:00pm'); SELECT CAST(SCOPE_IDENTITY() as int);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    id = (int)cmd.ExecuteScalar();
                }

                Restaurant restaurant = _restaurantDAL.GetRestaurant(id);
                Assert.AreEqual(id, restaurant.RestaurantId);
            }
        }
    }
}
