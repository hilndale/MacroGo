using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;
using System.Data.SqlClient;

namespace FinalCapstone.Dal
{
    public class FoodItemSqlDAL : IFoodItemDAL
    {
        private readonly string connectionString;

        public FoodItemSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddFoodItem(FoodList food)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO FOOD (Food_Item, Restaurant_Id, Calories, Total_Fat_g, Carbohydrates_g, Protein_g) VALUES (@Food_Item, @Restaurant_Id, @Calories, @Total_Fat_g, @Carbohydrates_g, @Protein_g);", conn);

                    cmd.Parameters.AddWithValue("@Food_Item", food.FoodId);
                    cmd.Parameters.AddWithValue("@Restaurant_Id", food.RestaurantId);
                    cmd.Parameters.AddWithValue("@Calories", food.Calories);
                    cmd.Parameters.AddWithValue("@Total_Fat_g", food.Fat);
                    cmd.Parameters.AddWithValue("@Carbohydrates_g", food.Carbs);
                    cmd.Parameters.AddWithValue("@Protein_g", food.Protein);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        result = true;
                    } 
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool DeleteFoodItem(FoodList food)
        {
            return false;
        }

        public bool UpdateFoodItem(FoodList food)
        {
            return false;
        }

    }
}

