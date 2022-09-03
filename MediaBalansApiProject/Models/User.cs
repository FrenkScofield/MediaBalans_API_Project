using MediaBalansApiProject.Models.Requests;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Models
{
    public class User : IdentityUser
    {
        public string  Firstname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public static implicit operator User(AddUserRequest v)
        {
            throw new NotImplementedException();
        }

    }
}
