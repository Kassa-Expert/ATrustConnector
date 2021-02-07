namespace KassaExpert.ATrustConnector.Lib.Credentials.Impl
{
    public sealed class Session : ICredentials
    {
        internal Session(string sessionId, string sessionKey, string username, string password)
        {
            SessionId = sessionId;
            SessionKey = sessionKey;
            Username = username;
            Password = password;
        }

        public string SessionId { get; }

        public string SessionKey { get; }

        internal string Username { get; }

        internal string Password { get; }
    }
}