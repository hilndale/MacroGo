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
        void AddToFavorites(UserFavorites userFavorite);
        void DeleteFromFavorites(UserFavorites userFavorite);
        IList<UserFavorites> GetFavorites(int userID);
    }
}
