namespace KassaExpert.ATrustConnector.Lib.ResponseDto
{
    public class CreateUserCertificateDto
    {
        internal CreateUserCertificateDto(string username, string password, byte[] certificate, string hex)
        {
            Username = username;
            Password = password;
            Certificate = certificate;
            SerialHex = hex;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] Certificate { get; set; }

        public string SerialHex { get; set; }
    }
}