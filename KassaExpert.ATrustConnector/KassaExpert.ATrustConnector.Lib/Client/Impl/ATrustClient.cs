using KassaExpert.ATrustConnector.Lib.Client.Impl.Request;
using KassaExpert.ATrustConnector.Lib.Client.Impl.Response;
using KassaExpert.ATrustConnector.Lib.Credentials.Impl;
using RestSharp;
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
    }
}