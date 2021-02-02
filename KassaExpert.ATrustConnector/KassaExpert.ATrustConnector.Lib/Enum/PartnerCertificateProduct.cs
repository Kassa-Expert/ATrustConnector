using Enum.Ext;

namespace KassaExpert.ATrustConnector.Lib.Enum
{
    public sealed class PartnerCertificateProduct : TypeSafeNameEnum<PartnerCertificateProduct, int>
    {
        public static readonly PartnerCertificateProduct HsmBasic = new PartnerCertificateProduct(1, "a.sign RK HSM Basic");
        public static readonly PartnerCertificateProduct HsmAdvanced = new PartnerCertificateProduct(2, "a.sign RK HSM Advanced");
        public static readonly PartnerCertificateProduct HsmPremium = new PartnerCertificateProduct(3, "a.sign RK HSM Premium");

        private PartnerCertificateProduct(int id, string name) : base(id, name)
        { }
    }
}