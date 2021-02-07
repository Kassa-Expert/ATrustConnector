using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Credentials;
using KassaExpert.Util.Lib.Dto;
using KassaExpert.Util.Lib.Enum;
using NUnit.Framework;
using System.Threading.Tasks;
using System;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public sealed class SignTests
    {
        private readonly IClient _client = new ATrustClient(true);

        private string _certHexSerial;

        [SetUp]
        public async Task Setup()
        {
            var response = await _client.GetCertificate("u123456789");

            _certHexSerial = response.Payload.SerialHex;
        }

        [Test]
        public async Task TestSignature_UserPassword()
        {
            var firstBeleg = new MachineReadableCode(TrustProvider.A_Trust, "BOX1", "1", DateTime.UtcNow, 0m, 0m, 0m, 0m, 0m, "00==", _certHexSerial, "prev===");

            var signature = await _client.Sign(firstBeleg, ICredentials.CreateByUsernamePassword("u123456789", "123456789"));

            signature.IsSuccessful.Should().BeTrue(because: signature.ErrorMessage);

            signature.Payload.Should().NotBeNull();
            signature.Payload.Signature.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task TestSignature_Session()
        {
            var session = await _client.CreateSession("u123456789", "123456789");

            var firstBeleg = new MachineReadableCode(TrustProvider.A_Trust, "BOX1", "1", DateTime.UtcNow, 0m, 0m, 0m, 0m, 0m, "00==", _certHexSerial, "prev===");

            var signature = await _client.Sign(firstBeleg, session.Payload);

            signature.IsSuccessful.Should().BeTrue(because: signature.ErrorMessage);

            signature.Payload.Should().NotBeNull();
            signature.Payload.Signature.Should().NotBeNullOrEmpty();
        }
    }
}