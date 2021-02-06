using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public class PartnerCertificateTests
    {
        private readonly IClient _client = new ATrustClient(true);

        [Test]
        public async Task CannotCreatePartnerCertificateWithoutCredits()
        {
            var response = await _client.CreatePartnerCertficate("partnerNoCredits", "partnerpwd",
                "no@no.com",
                Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345678",
                Lib.Enum.PartnerCertificateProduct.HsmAdvanced);

            response.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task CannotLoginWrongUser()
        {
            var response = await _client.CreatePartnerCertficate("partnerNoCredits1234", "partnerpwd",
                "no@no.com",
                Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345678",
                Lib.Enum.PartnerCertificateProduct.HsmAdvanced);

            response.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task CreateHsmBasic()
        {
            for (int i = 0; i < 10; i++)
            {
                var response = await _client.CreatePartnerCertficate("partner4711", "partnerpwd",
                    "no@no.com",
                    Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345678",
                    Lib.Enum.PartnerCertificateProduct.HsmBasic);

                response.IsSuccessful.Should().BeTrue(response.ErrorMessage);
            }
        }

        [Test]
        public async Task CreateHsmAdvanced()
        {
            var response = await _client.CreatePartnerCertficate("partner4711", "partnerpwd",
                "no@no.com",
                Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345688",
                Lib.Enum.PartnerCertificateProduct.HsmAdvanced);

            response.IsSuccessful.Should().BeTrue(response.ErrorMessage);
        }

        [Test]
        public async Task CreateHsmPremium()
        {
            var response = await _client.CreatePartnerCertficate("partner4711", "partnerpwd",
                "no@no.com",
                Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345698",
                Lib.Enum.PartnerCertificateProduct.HsmPremium);

            response.IsSuccessful.Should().BeTrue(response.ErrorMessage);
        }
    }
}