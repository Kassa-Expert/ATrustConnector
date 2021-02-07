namespace KassaExpert.ATrustConnector.Lib.Credentials.Impl
{
    public class User : ICredentials
    {
        internal User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        internal string Username { get; }

        internal string Password { get; }
    }
}