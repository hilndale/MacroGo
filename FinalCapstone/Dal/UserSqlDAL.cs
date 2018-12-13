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
                    user.UserId = conn.QueryFirst<int>("INSERT INTO Users (Email, Password, Goal_Fat, Goal_Protein, Goal_Carbs) VALUES (@emailValue, @password, @goalfat, @goalprotein, @goalcarbs); SELECT CAST(SCOPE_IDENTITY() as int);",
                        new { emailValue = user.Email, password = user.Password, goalfat = user.GoalFat, goalprotein = user.GoalProtein, goalcarbs = user.GoalCarbs });
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
