using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class User
    {
        public User(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public User(string username, string password, int id)
        {
            UserName = username;
            Password = password;
            Id = id;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
