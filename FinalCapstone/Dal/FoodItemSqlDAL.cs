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

        public bool AddFoodItem(FoodList food) //return int foodid
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO food (Food_Item, Restaurant_Id, Calories, Total_Fat_g, Carbohydrates_g, Protein_g) VALUES (@Food_Item, @Restaurant_Id, @Calories, @Total_Fat_g, @Carbohydrates_g, @Protein_g);", conn);

                    cmd.Parameters.AddWithValue("@Food_Item", food.FoodName);
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
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM food WHERE Food_Id = @Food_Id;", conn);
                    cmd.Parameters.AddWithValue("@Food_Id", food.FoodId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
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

        public bool UpdateFoodItem(FoodList food)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                   
                    SqlCommand cmd = new SqlCommand(@"UPDATE Food SET [Food_Item]=@Food_Item,[Restaurant_Id]=@Restaurant_Id, [Calories]=@Calories, [Total_Fat_g]=@Total_Fat_g, [Carbohydrates_g]=Carbohydrates_g, [Protein_g]=Protein_g WHERE Food_Id = @Food_Id;", conn);

                    cmd.Parameters.AddWithValue("@Food_Id", food.FoodId);
                    cmd.Parameters.AddWithValue("@Food_Item", food.FoodName);
                    cmd.Parameters.AddWithValue("@Restaurant_Id", food.RestaurantId);
                    cmd.Parameters.AddWithValue("@Calories", food.Calories);
                    cmd.Parameters.AddWithValue("@Total_Fat_g", food.Fat);
                    cmd.Parameters.AddWithValue("@Carbohydrates_g", food.Carbs);
                    cmd.Parameters.AddWithValue("@Protein_g", food.Protein);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
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

        public List<Item> GetAllFoodItems()
        {
            List<Item> AllItems = new List<Item>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "Select * From Food Join Restaurants On food.restaurant_id = restaurants.restaurant_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            FoodName = Convert.ToString(reader["Food_Item"]),
                            RestaurantName = Convert.ToString(reader["Restaurant_Name"]),
                            Protein = Convert.ToInt32(reader["Protein_g"]),
                            Fat = Convert.ToInt32(reader["Total_Fat_g"]),
                            Carbs = Convert.ToInt32(reader["Carbohydrates_g"]),
                            Calories = Convert.ToInt32(reader["Calories"])
                        };
                        AllItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AllItems;
        }
    }
}

