namespace KassaExpert.ATrustConnector.Lib.Credentials.Impl
{
    public sealed class Session : ICredentials
    {
        internal Session(string sessionId, string sessionKey)
        {
            SessionId = sessionId;
            SessionKey = sessionKey;
        }

        public string SessionId { get; }

        public string SessionKey { get; }
    }
}