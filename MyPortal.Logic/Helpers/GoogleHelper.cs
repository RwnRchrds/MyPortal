using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Configuration;

namespace MyPortal.Logic.Helpers
{
    internal class GoogleHelper
    {
        private GoogleConfig _google;

        public GoogleHelper(GoogleConfig google)
        {
            _google = google ?? throw new ConfigurationException(@"The Google Workspace configuration has not been added.");
        }

        public BaseClientService.Initializer GetInitializer(string accountName = null, params string[] scopes)
        {
            var credPath = _google.CredentialPath;

            if (string.IsNullOrWhiteSpace(accountName))
            {
                accountName = _google.DefaultAccountName;
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
