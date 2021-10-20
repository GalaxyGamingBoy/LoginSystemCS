using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.LoginSystem.Classes
{
    class User
    {
        public User()
        {
        }

        public User(string Username, string Password, string Country)
        {
            this.Username = Username;
            this.Password = Password;
            this.Country = Country;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
    }
}
