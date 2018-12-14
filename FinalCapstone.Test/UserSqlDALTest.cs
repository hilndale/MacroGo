using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalCapstone.Dal;
using System.Linq;
using System.Data.SqlClient;
using FinalCapstone.Models;
using System.Transactions;

namespace FinalCapstone.Test
{
    [TestClass]
    public class UserSqlDALTest : DatabaseTest
    {
        private TransactionScope tran;
        private IUserDAL _userDAL;

        [TestInitialize]
        public void Setup()
        {
            _userDAL = new UserSqlDAL(MacroGoConnectionString);
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void SaveUserTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int userID = (int)cmd.ExecuteScalar();
            }

            Users user = new Users
            {
                IsAdmin = 0,
                Email = "test@yahoo.com",
                Password = "password",
                GoalFat = 10,
                GoalProtein = 15,
                GoalCarbs = 2000
            };

            _userDAL.SaveUser(user);

            Assert.AreEqual("test@yahoo.com", user.Email);
        }

        [TestMethod]
        public void UpdateGoalsTest()
        {
            UserProfileViewModel user = new UserProfileViewModel()
            {
                Email = "test@yahoo.com",
                GoalCarbs = 100
            };

            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            bool result = _userDAL.UpdateGoals(user);
            Assert.AreEqual(true, result);
            Assert.AreEqual(100, user.GoalCarbs);
        }

        [TestMethod]
        public void GetUserProfile()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000);";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }

            Users comparison = new Users()
            {
                IsAdmin = 0,
                Email = "test@yahoo.com",
                Password = "password",
                GoalFat = 10,
                GoalProtein = 15,
                GoalCarbs = 2000
            };

            UserProfileViewModel result = _userDAL.GetUserProfile("test@yahoo.com");

            Assert.AreEqual(comparison.GoalCarbs, result.GoalCarbs);
        }

        [TestMethod]
        public void AddAdminTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sqlUser = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000);";

                SqlCommand cmd = new SqlCommand(sqlUser, conn);

                cmd.ExecuteNonQuery();

            }

            _userDAL.AddAdmin("test@yahoo.com");
            UserProfileViewModel result = _userDAL.GetUserProfile("test@yahoo.com");

            Assert.AreEqual(1, result.IsAdmin);
        }
    }


}
