using Enum.Ext;

namespace KassaExpert.ATrustConnector.Lib.Enum
{
    public sealed class PartnerCertificateClassification : TypeSafeNameEnum<PartnerCertificateClassification, int>
    {
        public static readonly PartnerCertificateClassification Uid = new PartnerCertificateClassification(0, "UID-Nummer");
        public static readonly PartnerCertificateClassification Gln = new PartnerCertificateClassification(1, "Global Location Number (GLN)");
        public static readonly PartnerCertificateClassification TaxId = new PartnerCertificateClassification(2, "Finanzamt- und Steuernummer");

        private PartnerCertificateClassification(int id, string name) : base(id, name)
        { }
    }
}