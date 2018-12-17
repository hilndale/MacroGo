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


        public bool AddToFavorites()
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO user_favorites ([User_Id], [Restaurant_Id], [Food_Id]) VALUES (user_Id, restaurant_id, food_id);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = cmd.ExecuteNonQuery();

                if (count == 1)
                {
                    result = true;
                }
            }
            return result;
        }

        public IList<UserFavorites> GetFavorites(int userID)
        {
            IList<UserFavorites> favorites = new List<UserFavorites>();

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
                    favorite.RestaurantId = Convert.ToInt32(reader["food_id"]);
                    favorite.FoodId = Convert.ToInt32(reader["food_id"]);
                    favorite.FoodName = Convert.ToString(reader["food_name"]);
                    favorite.Protein = Convert.ToInt32(reader["protein"]);
                    favorite.Fat = Convert.ToInt32(reader["fat"]);
                    favorite.Carbs = Convert.ToInt32(reader["carbs"]);
                    favorite.Calories = Convert.ToInt32(reader["calories"]); 
                }
            }
            return favorites;
        }


        //public bool DeleteFromFavorites()
        //{
        //    bool result = false;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string sql = "DELETE * FROM user_favorites WHERE ;";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        int count = cmd.ExecuteNonQuery();

        //        if (count == 1)
        //        {
        //            result = true;
        //        }
        //    }
        //    return result;
        //}

    }
}
