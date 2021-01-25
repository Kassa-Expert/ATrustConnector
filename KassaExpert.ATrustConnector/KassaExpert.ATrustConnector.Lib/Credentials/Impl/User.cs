using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.ATrustConnector.Lib.Credentials.Impl
{
    internal sealed class User : ICredentials
    {
        internal User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }
}