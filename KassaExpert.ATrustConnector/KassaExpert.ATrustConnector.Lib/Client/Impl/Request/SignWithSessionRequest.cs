namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Request
{
    internal sealed class SignWithSessionRequest
    {
        internal SignWithSessionRequest(string sessionKey, string payload)
        {
            sessionkey = sessionKey;
            jws_payload = payload;
        }

        public string sessionkey { get; set; }

        public string jws_payload { get; set; }
    }
}