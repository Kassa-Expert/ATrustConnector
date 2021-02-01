namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Request
{
    internal sealed class SignWithUserRequest
    {
        internal SignWithUserRequest(string pwd, string payload)
        {
            password = pwd;
            jws_payload = payload;
        }

        public string password { get; set; }

        public string jws_payload { get; set; }
    }
}