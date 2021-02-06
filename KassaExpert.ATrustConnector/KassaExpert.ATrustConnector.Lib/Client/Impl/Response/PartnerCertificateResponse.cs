using System.Collections.Generic;

namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Response
{
    internal class PartnerCertificateResponse
    {
        public string username { get; set; }

        public string password { get; set; }

        public string Signaturzertifikat { get; set; }

        public List<string> Zertifizierungsstellen { get; set; }

        public string Zertifikatsseriennummer { get; set; }

        public string ZertifikatsseriennummerHex { get; set; }

        public string alg { get; set; }
    }
}