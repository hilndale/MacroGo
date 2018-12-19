using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalCapstone.Utility;

namespace FinalCapstone.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int IsAdmin { get; set; }
        public string Password { get; set; }
        public int GoalFat { get; set; }
        public int GoalProtein { get; set; }
        public int GoalCarbs { get; set; }

        private byte[] salt = new byte[32];
        private byte[] hashedPassword = new byte[32];

        public byte[] Salt
        {
            get
            {
                byte[] result = new byte[32];
                for (int i = 0; i < salt.Length; i++)
                {
                    result[i] = salt[i];
                }
                return result;
            }
            set
            {
                for (int i = 0; i < salt.Length; i++)
                {
                    salt[i] = 0;
                }
                for (int i = 0; i < value.Length; i++)
                {
                    salt[i] = value[i];
                }
            }
        }

        public byte[] HashedPassword
        {
            get
            {
                byte[] result = new byte[32];
                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    result[i] = hashedPassword[i];
                }
                return result;
            }
            set
            {
                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    hashedPassword[i] = 0;
                }
                for (int i = 0; i < value.Length; i++)
                {
                    hashedPassword[i] = value[i];
                }
            }
        }

        public void HashPassword()
        {
            Salt = Hmac.GenerateSalt();
            HashedPassword = Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(Password), Salt);
            Password = string.Empty;
        }

        public byte[] HashChangedPassword(string newPassword)
        {
            //use salt already stored in user table
            HashedPassword = Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(newPassword), Salt);
            newPassword = string.Empty;
            return HashedPassword;
        }

        public bool CheckPassword(Users existingUser, string enteredPassword)
        {
            byte[] hashedEnteredPasswordBytes = Hmac.ComputeHMAC_SHA256(Encoding.UTF8.GetBytes(enteredPassword), existingUser.Salt);

            for (int i = 0; i < hashedEnteredPasswordBytes.Length; i++)
            {
                if (hashedEnteredPasswordBytes[i] != existingUser.HashedPassword[i])
                {
                    return false;
                }
            }

            return true;
        }


    }
}
