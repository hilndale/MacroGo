using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;
using Dapper;

namespace FinalCapstone.Dal
{
    public class UserSqlDAL : IUserDAL
    {
        private readonly string connectionString;

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Users GetUser(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Users result = conn.QueryFirstOrDefault<Users>("SELECT * FROM users WHERE Email = @emailValue", new { emailValue = email });
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void SaveUser(Users user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    user.UserId = conn.QueryFirst<int>("INSERT INTO Users (Is_Admin, Email, Password, Goal_Fat, Goal_Protein, Goal_Carbs) VALUES (0, @emailValue, @password, @goalfat, @goalprotein, @goalcarbs); SELECT CAST(SCOPE_IDENTITY() as int);",
                        new { emailValue = user.Email, password = user.Password, goalfat = user.GoalFat, goalprotein = user.GoalProtein, goalcarbs = user.GoalCarbs });
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdateGoals(UserProfileViewModel viewModel)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = ("UPDATE Users SET Goal_Fat = @goalfat, Goal_Protein = @goalprotein, Goal_Carbs = @goalcarbs WHERE email = @email;");
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@email", viewModel.Email.Replace("\"", ""));
                    cmd.Parameters.AddWithValue("@goalfat", viewModel.GoalFat);
                    cmd.Parameters.AddWithValue("@goalprotein", viewModel.GoalProtein);
                    cmd.Parameters.AddWithValue("@goalcarbs", viewModel.GoalCarbs);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public UserProfileViewModel GetUserProfile(string Email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    UserProfileViewModel result = conn.QueryFirstOrDefault<UserProfileViewModel>("SELECT * FROM users WHERE Email = @emailValue", new { emailValue = Email.Replace("\"", "") });
                    return result;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
