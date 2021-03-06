﻿namespace KassaExpert.ATrustConnector.Lib.Credentials
{
    public interface ICredentials
    {
        public static ICredentials CreateByUsernamePassword(string username, string password) => new Impl.User(username, password);

        public static ICredentials CreateBySessionIdKey(string sessionId, string sessionKey, string username, string password) => new Impl.Session(sessionId, sessionKey, username, password);
    }
}