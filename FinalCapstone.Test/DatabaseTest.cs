using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace FinalCapstone.Test
{
    [TestClass]
    public class DatabaseTest
    {
        private TransactionScope _transaction;

        [TestInitialize]
        public void DatabaseSetup()
        {
            _transaction = new TransactionScope();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();


            using (var connection = new SqlConnection(MacroGoConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM User_Favorites; 
                      DELETE FROM Users; 
                      DELETE FROM Food;
                      DELETE FROM Restaurants; ";

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void DatabaseCleanup()
        {
            _transaction.Dispose();
        }

        protected IConfiguration Configuration { get; private set; }
        //fix connection string after dependency injection 
        protected string MacroGoConnectionString
        {
            get
            {
                return Configuration["ConnectionStrings:MacroGo"];
            }
        }
    }
}
