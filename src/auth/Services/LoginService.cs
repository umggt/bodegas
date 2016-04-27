﻿using IdentityServer4.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Auth.Services
{
    public class LoginService
    {
        private readonly List<InMemoryUser> _users;

        public LoginService(List<InMemoryUser> users)
        {
            _users = users;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }
            return false;
        }

        public InMemoryUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
