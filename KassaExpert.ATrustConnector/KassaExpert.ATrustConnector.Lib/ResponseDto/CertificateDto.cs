using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.ATrustConnector.Lib.ResponseDto
{
    public class CertificateDto
    {
        internal CertificateDto(byte[] certificate, string hex)
        {
            Certificate = certificate;
            SerialHex = hex;
        }

        public byte[] Certificate { get; set; }

        public string SerialHex { get; set; }
    }
}