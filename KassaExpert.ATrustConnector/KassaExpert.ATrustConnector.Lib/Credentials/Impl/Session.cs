namespace KassaExpert.ATrustConnector.Lib.Credentials.Impl
{
    public sealed class Session : User
    {
        internal Session(string sessionId, string sessionKey, string username, string password) : base(username, password)
        {
            SessionId = sessionId;
            SessionKey = sessionKey;
        }

        public string SessionId { get; }

        public string SessionKey { get; }
    }
}