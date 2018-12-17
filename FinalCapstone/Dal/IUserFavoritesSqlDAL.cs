using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinalCapstone.Dal
{
    public interface IUserFavoritesSqlDAL
    {
        bool AddToFavorites();
        IList<UserFavorites> GetFavorites(int userID);
    }
}
