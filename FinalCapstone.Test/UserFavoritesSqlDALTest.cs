using System;
using System.Collections.Generic;
using System.Text;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using System.Linq;

namespace FinalCapstone.Test
{
    [TestClass]
    public class UserFavoritesSqlDALTest : DatabaseTest
    {
        private TransactionScope tran;
        private IUserFavoritesSqlDAL _userFavoritesSqlDAL;
        private IUserDAL _userDAL;
        private IRestaurantDAL _restaurantDAL;
        private IFoodItemDAL _foodItemDAL;
        private Users user;
        private int userId;
        private int restaurantId;
        private int foodId;

        [TestInitialize]
        public void Setup()
        {
            _userFavoritesSqlDAL = new UserFavoritesSqlDAL(MacroGoConnectionString);
            _userDAL = new UserSqlDAL(MacroGoConnectionString);
            _restaurantDAL = new RestaurantSqlDAL(MacroGoConnectionString);
            _foodItemDAL = new FoodItemSqlDAL(MacroGoConnectionString);
            tran = new TransactionScope();
            
            using(SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                userId = (int)cmd.ExecuteScalar();

                SqlCommand cmd2 = new SqlCommand("INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                restaurantId = (int)cmd2.ExecuteScalar();

                SqlCommand cmd3 = new SqlCommand("INSERT INTO food ([Food_Item], [Restaurant_Id], [Calories], [Total_Fat_g], [Carbohydrates_g], [Protein_g]) VALUES ('cheeseburger', @Restaurant_Id, 14, 12, 30, 45);SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                cmd3.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                foodId = (int)cmd3.ExecuteScalar();

            }
            Users user = new Users
            {
                IsAdmin = 0,
                Email = "test@yahoo.com",
                Password = "password",
                GoalFat = 10,
                GoalProtein = 15,
                GoalCarbs = 2000
            };
            _userDAL.SaveUser(user);
            
            Restaurant restaurant = _restaurantDAL.GetRestaurant(restaurantId);

            FoodList food = new FoodList
            {
                FoodId = foodId,
                FoodName = "Chips and Salsa",
                RestaurantId = restaurantId,
                Protein = 15,
                Fat = 5,
                Carbs = 10,
                Calories = 500
            };
            _foodItemDAL.AddFoodItem(food);

        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestClass]
        public class UserFavoritesTest : UserFavoritesSqlDALTest
        {
            [TestMethod]
            public void No_User_Favorites_Exist()
            {
                IList<UserFavorites> userFavorites = _userFavoritesSqlDAL.GetFavorites(userId);
                Assert.IsFalse(userFavorites.Any()); 
            }

            [TestMethod]
            public void AddFavoriteTest()
            {
                UserFavorites userFavorite = new UserFavorites
                {
                    Email = "test@yahoo.com",
                    UserId = userId,
                    RestaurantId = restaurantId,
                    RestaurantName = "Burger King",
                    FoodId = foodId,
                    FoodName = "cheeseburger",
                    Protein = 45,
                    Fat = 12,
                    Carbs = 30,
                    Calories = 14
                };

                _userFavoritesSqlDAL.AddToFavorites(userFavorite);
                Assert.AreEqual("cheeseburger", userFavorite.FoodName);
            }

            [TestMethod]
            public void GetFavoritesTest()
            {
                using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_favorites([User_Id], [Restaurant_Id], [Food_Id]) VALUES(@User_Id, @Restaurant_Id, @Food_Id);", conn);
                    cmd.Parameters.AddWithValue("@User_Id", userId);
                    cmd.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                    cmd.Parameters.AddWithValue("Food_Id", foodId);
                    cmd.ExecuteNonQuery(); 
                }
                IList<UserFavorites> userFavorites = _userFavoritesSqlDAL.GetFavorites(userId);
                Assert.AreEqual(1, userFavorites.Count);
            }
        }

    }
}
