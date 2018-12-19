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
        bool UpdateGoals(UserProfileViewModel viewModel);
        UserProfileViewModel GetUserProfile(string email);
        bool ChangePassword(string email, byte[] newHashedPassword);
        bool IsAdmin(string Email);
        void AddAdmin(string email);
    }
}
