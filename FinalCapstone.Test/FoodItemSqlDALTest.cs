using System;
using System.Collections.Generic;
using System.Text;
using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;

namespace FinalCapstone.Test
{

    //need to add restaurant for integration test; waiting on update/delete DALs - then test
    [TestClass]
    public class FoodItemSqlDALTest : DatabaseTest
    {
        private TransactionScope tran;
        private IFoodItemDAL _foodItemDAL;
        private int restaurantId;
        private int foodId;

        [TestInitialize]
        public void Setup()
        {
            _foodItemDAL = new FoodItemSqlDAL(MacroGoConnectionString);

            tran = new TransactionScope();
        }

        // Rollback the existing transaction.
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddFoodItemTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql1 = "INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                restaurantId = (int)cmd1.ExecuteScalar();
            }
            FoodList food = new FoodList
            {
                FoodName = "Chips and Salsa",
                RestaurantId = restaurantId,
                Protein = 15,
                Fat = 5,
                Carbs = 10,
                Calories = 500
            };
            bool result = _foodItemDAL.AddFoodItem(food);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteFoodItemTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sqlRest = "INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sqlRest, conn);
                restaurantId = (int)cmd.ExecuteScalar();

                string sqlFood = "INSERT INTO food ([Food_Item], [Restaurant_Id], [Calories], [Total_Fat_g], [Carbohydrates_g], [Protein_g]) VALUES ('cheeseburger', @Restaurant_Id, 14, 12, 30, 45);SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd2 = new SqlCommand(sqlFood, conn);
                cmd2.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                foodId = (int)cmd2.ExecuteScalar();
            }

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

            bool didDelete = _foodItemDAL.DeleteFoodItem(food);
            Assert.AreEqual(true, didDelete);
        }

        [TestMethod]
        public void UpdateFoodItemTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sqlRest = "INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sqlRest, conn);
                restaurantId = (int)cmd.ExecuteScalar();

                string sqlFood = "INSERT INTO food ([Food_Item], [Restaurant_Id], [Calories], [Total_Fat_g], [Carbohydrates_g], [Protein_g]) VALUES ('cheeseburger', @Restaurant_Id, 14, 12, 30, 45);SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd2 = new SqlCommand(sqlFood, conn);
                cmd2.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                foodId = (int)cmd2.ExecuteScalar();

            }
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

            FoodList foodUpdate = new FoodList
            {
                FoodId = foodId,
                FoodName = "Chips and queso",
                RestaurantId = restaurantId,
                Protein = 20,
                Fat = 10,
                Carbs = 17,
                Calories = 40
            };

            bool result = _foodItemDAL.UpdateFoodItem(foodUpdate); 
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetFoodItemsTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sqlRest = "INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sqlRest, conn);
                restaurantId = (int)cmd.ExecuteScalar();

                string sqlFood = "INSERT INTO food ([Food_Item], [Restaurant_Id], [Calories], [Total_Fat_g], [Carbohydrates_g], [Protein_g]) VALUES ('cheeseburger', @Restaurant_Id, 14, 12, 30, 45);SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd2 = new SqlCommand(sqlFood, conn);
                cmd2.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                foodId = (int)cmd2.ExecuteScalar();

            }

            IList<Item> results = _foodItemDAL.GetAllFoodItems();
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void GetFoodItemTest()
        {
            using(SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sqlRest = "INSERT INTO restaurants ([Restaurant_Name], [Open_Time], [Close_Time]) VALUES ('Burger King', '6:00AM', '11:00PM'); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sqlRest, conn);
                restaurantId = (int)cmd.ExecuteScalar();

                string sqlFood = "INSERT INTO food ([Food_Item], [Restaurant_Id], [Calories], [Total_Fat_g], [Carbohydrates_g], [Protein_g]) VALUES ('cheeseburger', @Restaurant_Id, 14, 12, 30, 45);SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd2 = new SqlCommand(sqlFood, conn);
                cmd2.Parameters.AddWithValue("@Restaurant_Id", restaurantId);
                foodId = (int)cmd2.ExecuteScalar();
            }

            FoodList result = _foodItemDAL.GetFood(foodId);
            Assert.AreEqual(foodId, result.FoodId);
        }
    }
}


