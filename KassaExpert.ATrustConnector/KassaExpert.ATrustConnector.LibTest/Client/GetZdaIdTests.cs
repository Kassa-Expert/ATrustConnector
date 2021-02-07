using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public class GetZdaIdTests
    {
        private readonly IClient _client = new ATrustClient(true);

        [Test]
        public async Task GetZdaId()
        {
            var response = await _client.GetZdaId("u039193334");

            response.IsSuccessful.Should().BeTrue();

            response.Payload.Should().NotBeNullOrEmpty();
            response.Payload.Should().Be("AT1");
        }
    }
}