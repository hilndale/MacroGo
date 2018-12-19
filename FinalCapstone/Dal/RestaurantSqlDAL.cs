using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;
using System.Data.SqlClient;

namespace FinalCapstone.Dal
{
    public class RestaurantSqlDAL : IRestaurantDAL
    {
        private readonly string connectionString;

        public RestaurantSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Restaurants;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Restaurant RestaurantSQL = new Restaurant
                        {
                            RestaurantId = Convert.ToInt32(reader["Restaurant_Id"]),
                            RestaurantName = Convert.ToString(reader["Restaurant_Name"]),
                            OpenTime = Convert.ToString(reader["Open_Time"]),
                            CloseTime = Convert.ToString(reader["Close_Time"]),

                        };

                        restaurants.Add(RestaurantSQL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return restaurants;
        }

        public Restaurant GetRestaurant(int id)
        {
            Restaurant restaurant = new Restaurant();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Restaurants WHERE Restaurant_Id = @id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        {
                            restaurant.RestaurantId = Convert.ToInt32(reader["Restaurant_Id"]);
                            restaurant.RestaurantName = Convert.ToString(reader["Restaurant_Name"]);
                            restaurant.OpenTime = Convert.ToString(reader["Open_Time"]);
                            restaurant.CloseTime = Convert.ToString(reader["Close_Time"]);

                        };
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return restaurant;
        }

        public bool AddRestaurant(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Restaurants (Restaurant_name, Open_time, close_time) VALUES (@name, @openTime, @closeTime);", conn);

                    cmd.Parameters.AddWithValue("@name", restaurant.RestaurantName);
                    cmd.Parameters.AddWithValue("@openTime", restaurant.OpenTime);
                    cmd.Parameters.AddWithValue("@closeTime", restaurant.CloseTime);

                    cmd.ExecuteNonQuery();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

