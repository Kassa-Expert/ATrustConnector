using KassaExpert.ATrustConnector.Lib.Enum;

namespace KassaExpert.ATrustConnector.Lib.Client.Impl.Request
{
    internal sealed class PartnerCertificateRequest
    {
        internal PartnerCertificateRequest(string password, string mail, PartnerCertificateClassification classification, string classificationValue, PartnerCertificateProduct product)
        {
            partner_password = password;
            email = mail;
            classification_key_type = classification.Id;
            classification_key = classificationValue;
            product_version = product.Id;
        }

        public string partner_password { get; set; }

        public int classification_key_type { get; set; }

        public string classification_key { get; set; }

        public string email { get; set; }

        public int product_version { get; set; }
    }
}