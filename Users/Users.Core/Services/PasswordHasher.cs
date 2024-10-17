﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Interfaces;

namespace Users.Core.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool VerifyHashedPassword(string providedPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword);
    }
}
