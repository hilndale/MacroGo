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
                IsAdmin = false,
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
        public void SaveAdminTest()
        {
            using (SqlConnection conn = new SqlConnection(MacroGoConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (1, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int userID = (int)cmd.ExecuteScalar();
            }

            Users user = new Users
            {
                IsAdmin = true,
                Email = "test@yahoo.com",
                Password = "password",
                GoalFat = 10,
                GoalProtein = 15,
                GoalCarbs = 2000
            };

            _userDAL.SaveUser(user);

            Assert.AreEqual(true, user.IsAdmin);
        }


        //public void UpdateGoalsTest()
        //{
        //    using(SqlConnection conn = new SqlConnection(MacroGoConnectionString))
        //    {
        //        conn.Open();

        //        string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        UserProfileViewModel user = (UserProfileViewModel)cmd.ExecuteScalar(); 
        //    }

        //    bool result = _userDAL.UpdateGoals(user);
        //}

        //public void GetUserTest()
        //{
        //    using(SqlConnection conn = new SqlConnection(MacroGoConnectionString))
        //    {
        //        conn.Open();

        //        string sql = "INSERT INTO users ([Is_Admin], [Email], [Password], [Goal_Fat], [Goal_Protein], [Goal_Carbs]) VALUES (0, 'test@yahoo.com', 'password', 10, 15, 2000); SELECT CAST(SCOPE_IDENTITY() as int);";
        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //       int id = (int)cmd.ExecuteScalar();

        //    }

        //    Users user =  _userDAL.GetUser(email);


        //}

    }


}
