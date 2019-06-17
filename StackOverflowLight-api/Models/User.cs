using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public User(string firstname,string lastname,string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
        public User()
        {

        }
    }
}
