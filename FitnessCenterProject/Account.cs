using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterProject
{
    public abstract class Account
    {
        public abstract string Username { get; set; }
        public abstract string Password { get; set; }


        public abstract void Register();

        public virtual bool ValidateInput(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && password.Length >= 6;
        }
    }
}
