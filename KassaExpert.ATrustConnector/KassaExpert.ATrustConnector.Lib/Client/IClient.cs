using KassaExpert.ATrustConnector.Lib.Client.Impl;
using KassaExpert.ATrustConnector.Lib.Credentials.Impl;
using KassaExpert.ATrustConnector.Lib.Credentials;
using KassaExpert.ATrustConnector.Lib.Enum;
using KassaExpert.ATrustConnector.Lib.ResponseDto;
using KassaExpert.Util.Lib.Dto;
using System.Threading.Tasks;

namespace KassaExpert.ATrustConnector.Lib.Client
{
    /// <summary>
    /// http://labs.a-trust.at/developer/pdf/asignRKHSM_basic_advanced_premium.pdf
    /// </summary>
    public interface IClient
    {
        public static IClient CreateATrustClient() => new ATrustClient();

        #region SESSION

        Task<IResponse<Session>> CreateSession(string username, string password);

        Task<IResponse> DeleteSession(string sessionId);

        #endregion SESSION

        #region SIGN

        Task<IResponse<JwsItem>> Sign(MachineReadableCode data, ICredentials credentials);

        #endregion SIGN

        Task<IResponse<CertificateDto>> GetCertificate(string username);

        Task<IResponse<string>> GetZdaId(string username);

        Task<IResponse> ChangePassword(string username, string oldPassword, string newPassword);

        Task<IResponse<CreateUserCertificateDto>> CreatePartnerCertficate(string partner_user, string partner_password, string mail, PartnerCertificateClassification classificationType, string classificationValue, PartnerCertificateProduct product);
    }
}