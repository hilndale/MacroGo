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

        [TestInitialize]
        public void Setup()
        {

            _foodItemDAL = new FoodItemSqlDAL(MacroGoConnectionString);

            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO Food ([Food_Item],[Restaurant_Id],[Calories],[Total_Fat_g],[Carbohydrates_g],[Protein_g])" +
                    " VALUES ('Cheesy Bean and Rice Burrito', 1, 425, 25, 40, 20)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteScalar();
            }
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
            FoodItemSqlDAL foodItemSqlDAL = new FoodItemSqlDAL(MacroGoConnectionString);
            FoodList food = new FoodList
            {
                FoodName = "Chips and Salsa",
                RestaurantId = 1,
                Protein = 15,
                Fat = 5,
                Carbs = 10,
                Calories = 500
            };

            bool result = foodItemSqlDAL.AddFoodItem(food);
            Assert.IsTrue(result);
        }

    }
}
