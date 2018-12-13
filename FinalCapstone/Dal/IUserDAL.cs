﻿using System;
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
        void SaveAdmin(Users user);
        bool UpdateGoals(UserProfileViewModel viewModel);
        UserProfileViewModel GetUserProfile(string email);
        bool ChangePassword(string username, string newPassword);
        bool IsAdmin(string Email);
    }
}
