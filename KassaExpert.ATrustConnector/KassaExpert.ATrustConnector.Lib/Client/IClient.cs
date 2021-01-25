﻿using KassaExpert.ATrustConnector.Lib.Client.Impl;
using KassaExpert.ATrustConnector.Lib.Credentials.Impl;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}