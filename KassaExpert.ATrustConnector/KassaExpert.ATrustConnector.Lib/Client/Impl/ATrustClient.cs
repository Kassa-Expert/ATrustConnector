﻿using KassaExpert.ATrustConnector.Lib.Client.Impl.Request;
using KassaExpert.ATrustConnector.Lib.Client.Impl.Response;
using KassaExpert.ATrustConnector.Lib.Credentials.Impl;
using KassaExpert.ATrustConnector.Lib.Credentials;
using KassaExpert.ATrustConnector.Lib.Enum;
using KassaExpert.ATrustConnector.Lib.ResponseDto;
using KassaExpert.Util.Lib.Dto;
using RestSharp;
using System.Threading.Tasks;
using System;

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
                return new Response<Session>(false, null, response.StatusDescription);
            }

            return new Response<Session>(true, new Session(response.Data.sessionid, response.Data.sessionkey, username, password), null);
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
            if (data.Signature is not null)
            {
                return new Response<JwsItem>(false, null, "MachineReadableCode should not have a signature");
            }

            if (credentials is Session session)
            {
                return await SignWithSession(data, session);
            }

            if (credentials is User user)
            {
                return await SignWithUser(data, user);
            }

            return new Response<JwsItem>(false, null, "Credentials are not of type User or Session, create with ICredentials");
        }

        private async Task<IResponse<JwsItem>> SignWithSession(MachineReadableCode data, Session credentials)
        {
            var request = new RestRequest("Session/{sessionId}/Sign/JWS", Method.POST, DataFormat.Json);

            request.AddUrlSegment("sessionId", credentials.SessionId);

            request.AddJsonBody(new SignWithSessionRequest(credentials.SessionKey, data.GetCode()));

            var response = await _client.ExecuteAsync<SignWithSessionResponse>(request);

            if (!response.IsSuccessful)
            {
                //SESSION CAN BE INACTIVE -> SIGN WITH USER/PASSWORD
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return await SignWithUser(data, credentials);
                }

                return new Response<JwsItem>(false, null, response.StatusDescription);
            }

            return new Response<JwsItem>(true, new JwsItem(response.Data.result), null);
        }

        private async Task<IResponse<JwsItem>> SignWithUser(MachineReadableCode data, User credentials)
        {
            var request = new RestRequest("{Benutzername}/Sign/JWS", Method.POST, DataFormat.Json);

            request.AddUrlSegment("Benutzername", credentials.Username);

            request.AddJsonBody(new SignWithUserRequest(credentials.Password, data.GetCode()));

            var response = await _client.ExecuteAsync<SignWithUserResponse>(request);

            if (!response.IsSuccessful)
            {
                return new Response<JwsItem>(false, null, response.StatusDescription);
            }

            return new Response<JwsItem>(true, new JwsItem(response.Data.result), null);
        }

        #endregion SIGN

        public async Task<IResponse<CertificateDto>> GetCertificate(string username)
        {
            var request = new RestRequest("{Benutzername}/Certificate", Method.GET);

            request.AddUrlSegment("Benutzername", username);

            var response = await _client.ExecuteAsync<CertificateResponse>(request);

            if (!response.IsSuccessful)
            {
                return new Response<CertificateDto>(false, null, response.StatusDescription);
            }

            var returnDto = new CertificateDto(Convert.FromBase64String(response.Data.Signaturzertifikat), response.Data.ZertifikatsseriennummerHex);

            return new Response<CertificateDto>(true, returnDto, null);
        }

        public async Task<IResponse<string>> GetZdaId(string username)
        {
            var request = new RestRequest("{Benutzername}/ZDA", Method.GET);

            request.AddUrlSegment("Benutzername", username);

            var response = await _client.ExecuteAsync<ZdaResponse>(request);

            if (!response.IsSuccessful)
            {
                return new Response<string>(false, null, response.StatusDescription);
            }

            return new Response<string>(true, response.Data.zdaid, null);
        }

        public async Task<IResponse> ChangePassword(string username, string oldPassword, string newPassword)
        {
            var request = new RestRequest("{Benutzername}/Password", Method.POST, DataFormat.Json);

            request.AddUrlSegment("Benutzername", username);

            request.AddJsonBody(new ChangePasswordRequest(oldPassword, newPassword));

            var response = await _client.ExecuteAsync<ChangePasswordResponse>(request);

            if (!response.IsSuccessful)
            {
                return new ResponseDto.Response(false, response.StatusDescription);
            }

            if (!response.Data.result)
            {
                return new ResponseDto.Response(false, "Passwort konnte nicht geändert werden");
            }

            return new ResponseDto.Response(true, null);
        }

        public async Task<IResponse<CreateUserCertificateDto>> CreatePartnerCertficate(string partner_user, string partner_password, string mail, PartnerCertificateClassification classificationType, string classificationValue, PartnerCertificateProduct product)
        {
            var request = new RestRequest("{partner_benutzername}/Account", Method.POST, DataFormat.Json);

            request.AddUrlSegment("partner_benutzername", partner_user.Replace(" ", string.Empty));

            request.AddJsonBody(new PartnerCertificateRequest(partner_password, mail, classificationType, classificationValue, product));

            var response = await _client.ExecuteAsync<PartnerCertificateResponse>(request);

            if (!response.IsSuccessful)
            {
                return new Response<CreateUserCertificateDto>(false, null, response.StatusDescription);
            }

            return new Response<CreateUserCertificateDto>(true, new CreateUserCertificateDto(response.Data.username, response.Data.password, Convert.FromBase64String(response.Data.Signaturzertifikat), response.Data.ZertifikatsseriennummerHex), null);
        }
    }
}