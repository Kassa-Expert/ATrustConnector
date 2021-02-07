using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using KassaExpert.Util.Lib.Validation;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public class PartnerCertificateTests
    {
        private readonly IClient _client = new ATrustClient(true);

        private readonly IValidation _validation = IValidation.GetInstance();

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
            var response = await _client.CreatePartnerCertficate("partner4711", "partnerpwd",
                "no@no.com",
                Lib.Enum.PartnerCertificateClassification.Uid, "ATU12345678",
                Lib.Enum.PartnerCertificateProduct.HsmBasic);

            response.IsSuccessful.Should().BeTrue(response.ErrorMessage);

            response.Payload.Should().NotBeNull();

            response.Payload.Username.Should().NotBeNullOrEmpty();
            response.Payload.Password.Should().NotBeNullOrEmpty();
            response.Payload.Certificate.Should().NotBeNullOrEmpty();
            _validation.IsValidHexSerial(response.Payload.SerialHex).Should().BeTrue();
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