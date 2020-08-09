using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Helpers
{
    public class GoogleHelper
    {
        private string[] _scopes;
        private IConfiguration _config;

        public GoogleHelper(IConfiguration config)
        {
            _config = config;

            _scopes = new[]
            {
                DriveService.Scope.Drive
            };
        }

        public BaseClientService.Initializer GetInitializer(string accountName = null)
        {
            var credPath = _config.GetValue<string>("GSuiteIntegration:CredentialPath");

            if (string.IsNullOrWhiteSpace(accountName))
            {
                accountName = _config.GetValue<string>("GSuiteIntegration:DefaultAccountName");
            }

            var originCredential =
                (ServiceAccountCredential)GoogleCredential.FromFile(credPath)
                    .UnderlyingCredential;

            var initializer = new ServiceAccountCredential.Initializer(originCredential.Id)
            {
                User = accountName,
                Key = originCredential.Key,
                Scopes = _scopes
            };

            var credential = new ServiceAccountCredential(initializer);

            return new BaseClientService.Initializer
            {
                ApplicationName = "MyPortal",
                HttpClientInitializer = credential
            };
        }
    }
}
