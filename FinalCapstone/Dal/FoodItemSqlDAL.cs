using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class FoodItemSqlDAL : IFoodItemDAL
    {
        private readonly string connectionString;

        public FoodItemSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}

