using Dapper;
using FinalCapstone.Models;
using System;
using System.Data.SqlClient;

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

        public bool ChangePassword(string email, string newPassword)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = ("UPDATE Users SET password = @password WHERE email = @email;");
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@email", email.Replace("\"", ""));
                    cmd.Parameters.AddWithValue("@password", newPassword);

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
            UserProfileViewModel viewModel = new UserProfileViewModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM users WHERE Email = @emailValue;";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@emailValue", Email.Replace("\"", ""));

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        viewModel.GoalCarbs = Convert.ToInt32(reader["Goal_Carbs"]);
                        viewModel.GoalProtein = Convert.ToInt32(reader["Goal_Protein"]);
                        viewModel.GoalFat = Convert.ToInt32(reader["Goal_Fat"]);
                    }
                    return viewModel;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
