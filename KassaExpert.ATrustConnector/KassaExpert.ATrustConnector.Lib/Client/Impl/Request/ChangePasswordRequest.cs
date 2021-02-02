namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Request
{
    internal sealed class ChangePasswordRequest
    {
        internal ChangePasswordRequest(string oldPassword, string newPassword)
        {
            currentpassword = oldPassword;
            newpassword = newPassword;
        }

        public string currentpassword { get; set; }

        public string newpassword { get; set; }
    }
}