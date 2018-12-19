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

        //Delete if possible
        public Users GetUser(string email)
        {
            Users user = new Users();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM users WHERE Email = @emailValue;";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@emailValue", email.Replace("\"", ""));

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt32(reader["User_id"]);
                        user.Email = Convert.ToString(reader["email"]);
                        user.IsAdmin = Convert.ToInt32(reader["Is_Admin"]);
                        user.Password = Convert.ToString(reader["password"]);
                        user.GoalCarbs = Convert.ToInt32(reader["Goal_Carbs"]);
                        user.GoalProtein = Convert.ToInt32(reader["Goal_Protein"]);
                        user.GoalFat = Convert.ToInt32(reader["Goal_Fat"]);
                        user.Salt = (byte[])reader["Salt"];
                        user.HashedPassword = (byte[])reader["Hashed_Password"];
                    }
                    return user;
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
                    user.UserId = conn.QueryFirst<int>("INSERT INTO Users (Is_Admin, Email, Password, Goal_Fat, Goal_Protein, Goal_Carbs, Salt, Hashed_Password) VALUES (0, @emailValue, @password, @goalfat, @goalprotein, @goalcarbs, @salt, @hashedpassword); SELECT CAST(SCOPE_IDENTITY() as int);",
                        new { emailValue = user.Email, password = user.Password, goalfat = user.GoalFat, goalprotein = user.GoalProtein, goalcarbs = user.GoalCarbs, salt = user.Salt, hashedpassword = user.HashedPassword });
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

        public bool ChangePassword(string email, byte[] newHashedPassword)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = ("UPDATE Users SET hashed_password=@hashedpassword WHERE email = @email;");
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@email", email.Replace("\"", ""));
                    cmd.Parameters.AddWithValue("@hashedpassword", newHashedPassword);

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
                        viewModel.UserId = Convert.ToInt32(reader["User_Id"]);
                        viewModel.Email = Convert.ToString(reader["Email"]);
                        viewModel.IsAdmin = Convert.ToInt32(reader["Is_Admin"]);
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

        public bool IsAdmin(string Email)
        {
            Users user = new Users();

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
                        user.IsAdmin = Convert.ToInt32(reader["Is_Admin"]);
                    }

                    if (user.IsAdmin == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void AddAdmin(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE users SET Is_Admin = 1 WHERE email = @email;";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@email", email.Replace("\"", ""));
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
