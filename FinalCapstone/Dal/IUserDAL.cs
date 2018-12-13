using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IUserDAL
    {
        Users GetUser(string email);
        void SaveUser(Users user);
        UserProfileViewModel GetUserProfile(string email);
    }
}
