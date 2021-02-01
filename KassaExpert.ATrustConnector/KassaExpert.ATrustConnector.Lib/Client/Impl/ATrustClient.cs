using KassaExpert.ATrustConnector.Lib.Client.Impl.Request;
using KassaExpert.ATrustConnector.Lib.Client.Impl.Response;
using KassaExpert.ATrustConnector.Lib.Credentials;
using KassaExpert.ATrustConnector.Lib.Credentials.Impl;
using KassaExpert.Util.Lib.Dto;
using RestSharp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.Lib.Client.Impl
{
    /// <summary>
    /// http://labs.a-trust.at/developer/pdf/asignRKHSM_basic_advanced_premium.pdf
    /// </summary>
    internal sealed class ATrustClient : IClient
    {
        private readonly IRestClient _client;

        internal ATrustClient(bool isTest = false)
        {
            if (isTest)
            {
                _client = new RestClient("https://hs-abnahme.a-trust.at/asignrkonline/v2");
            }
            else
            {
                _client = new RestClient("https://www.a-trust.at/asignrkonline/v2");
            }
        }

        #region SESSION

        public async Task<IResponse<Session>> CreateSession(string username, string password)
        {
            var request = new RestRequest("Session/{Benutzername}", Method.PUT, DataFormat.Json);

            request.AddUrlSegment("Benutzername", username);

            request.AddJsonBody(new SessionRequest
            {
                password = password
            });

            var response = await _client.ExecuteAsync<SessionResponse>(request);

            //TODO: calc expiration-date (1h from request, max to midnight)
            if (!response.IsSuccessful)
            {
                return new ResponseDto.Response<Session>(false, null, response.StatusDescription);
            }

            return new ResponseDto.Response<Session>(true, new Session(response.Data.sessionid, response.Data.sessionkey), null);
        }

        public async Task<IResponse> DeleteSession(string sessionId)
        {
            var request = new RestRequest("Session/{sessionId}", Method.DELETE);

            request.AddUrlSegment("sessionId", sessionId);

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                return new ResponseDto.Response(false, response.StatusDescription);
            }

            return new ResponseDto.Response(true, null);
        }

        #endregion SESSION

        #region SIGN

        public async Task<IResponse<JwsItem>> Sign(MachineReadableCode data, ICredentials credentials)
        {
            if (data.Signature is null)
            {
                return new ResponseDto.Response<JwsItem>(false, null, "MachineReadableCode should not have a signature");
            }

            if (credentials is Session session)
            {
                return await SignWithSession(data, session);
            }

            if (credentials is User user)
            {
                return await SignWithUser(data, user);
            }

            return new ResponseDto.Response<JwsItem>(false, null, "Credentials are not of type User or Session, create with ICredentials");
        }

        private async Task<IResponse<JwsItem>> SignWithSession(MachineReadableCode data, Session credentials)
        {
            return null;
        }

        private async Task<IResponse<JwsItem>> SignWithUser(MachineReadableCode data, User credentials)
        {
            return null;
        }

        #endregion SIGN
    }
}