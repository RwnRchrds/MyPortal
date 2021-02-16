using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace MyPortal.Logic.Helpers
{
    public class GoogleHelper
    {
        private IConfiguration _config;

        public GoogleHelper(IConfiguration config)
        {
            _config = config;
        }

        public BaseClientService.Initializer GetInitializer(string accountName = null, params string[] scopes)
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
                Scopes = scopes
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
