using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using KassaExpert.Util.Lib.Validation;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public class GetCertificateTests
    {
        private readonly IClient _client = new ATrustClient(true);

        private readonly IValidation _validation = IValidation.GetInstance();

        [Test]
        public async Task GetCertificate()
        {
            var response = await _client.GetCertificate("u039193334");

            response.IsSuccessful.Should().BeTrue(because: response.ErrorMessage);

            response.Payload.Should().NotBeNull();

            response.Payload.SerialHex.Should().NotBeNullOrEmpty();
            _validation.IsValidHexSerial(response.Payload.SerialHex).Should().BeTrue();

            response.Payload.Certificate.Should().NotBeNullOrEmpty();
        }
    }
}