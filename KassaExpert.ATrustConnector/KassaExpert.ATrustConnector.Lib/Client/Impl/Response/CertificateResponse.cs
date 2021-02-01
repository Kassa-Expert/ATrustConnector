namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Response
{
    internal sealed class CertificateResponse
    {
        public string Signaturzertifikat { get; set; }

        public string[] Zertifizierungsstellen { get; set; }

        public string Zertifikatsseriennummer { get; set; }

        public string ZertifikatsseriennummerHex { get; set; }

        public string alg { get; set; }
    }
}