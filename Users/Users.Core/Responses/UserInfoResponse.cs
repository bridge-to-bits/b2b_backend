using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Models;

namespace Users.Core.Responses
{
    public class UserInfoResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string Avatar { get; set; }
    }
}
