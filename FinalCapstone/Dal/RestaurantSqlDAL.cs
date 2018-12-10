using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class RestaurantSqlDAL : IRestaurantDAL
    {
        private readonly string connectionString;

        public RestaurantSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}

