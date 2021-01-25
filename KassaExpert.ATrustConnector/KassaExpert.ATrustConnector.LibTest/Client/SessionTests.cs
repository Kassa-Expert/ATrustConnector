using FluentAssertions;
using KassaExpert.ATrustConnector.Lib.Client;
using KassaExpert.ATrustConnector.Lib.Client.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.LibTest.Client
{
    [TestFixture]
    public class SessionTests
    {
        private readonly IClient _client = new ATrustClient(true);

        [Test]
        public async Task CreateSessionShouldFailTest()
        {
            var session = await _client.CreateSession("u039193334", "60z7dx");

            session.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task CreateSessionShouldPassTest()
        {
            var session = await _client.CreateSession("u123456789", "123456789");

            session.IsSuccessful.Should().BeTrue();

            session.Payload.Should().NotBeNull();
            session.Payload!.SessionId.Should().NotBeNull();
            session.Payload!.SessionKey.Should().NotBeNull();
        }
    }
}