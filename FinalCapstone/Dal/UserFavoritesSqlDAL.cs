using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class UserFavoritesSqlDAL : IUserFavoritesSqlDAL
    {
        private readonly string connectionString;

        public UserFavoritesSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddToFavorites(UserFavorites userFavorite)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO user_favorites ([User_Id], [Restaurant_Id], [Food_Id]) VALUES (@user_Id, @restaurant_id, @food_id);", conn);
                    cmd.Parameters.AddWithValue("@user_id", userFavorite.UserId);
                    cmd.Parameters.AddWithValue("@restaurant_id", userFavorite.RestaurantId);
                    cmd.Parameters.AddWithValue("@food_id", userFavorite.FoodId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public IList<UserFavorites> GetFavorites(int userID)
        {
            IList<UserFavorites> favorites = new List<UserFavorites>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM user_favorites uf JOIN food f ON uf.Food_Id = f.Food_Id JOIN Restaurants r ON f.Restaurant_Id = r.Restaurant_Id WHERE user_id = @user_id;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserFavorites favorite = new UserFavorites();

                        favorite.RestaurantName = Convert.ToString(reader["restaurant_name"]);
                        favorite.RestaurantId = Convert.ToInt32(reader["restaurant_id"]);
                        favorite.FoodId = Convert.ToInt32(reader["food_id"]);
                        favorite.FoodName = Convert.ToString(reader["food_item"]);
                        favorite.Protein = Convert.ToInt32(reader["protein_g"]);
                        favorite.Fat = Convert.ToInt32(reader["Total_fat_g"]);
                        favorite.Carbs = Convert.ToInt32(reader["carbohydrates_g"]);
                        favorite.Calories = Convert.ToInt32(reader["calories"]);

                        favorites.Add(favorite);
                    }
                }
                return favorites;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void DeleteFromFavorites(UserFavorites userFavorite)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM user_favorites WHERE @User_Id = User_Id AND @Food_Id = Food_Id ;", conn);
                    cmd.Parameters.AddWithValue("@User_Id", userFavorite.UserId);
                    cmd.Parameters.AddWithValue("@Food_Id", userFavorite.FoodId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
