using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    internal class GoogleHelper
    {
        internal static BaseClientService.Initializer GetInitializer(string accessToken, params string[] scopes)
        {
            var originCredential =
                (ServiceAccountCredential)GoogleCredential.FromAccessToken(accessToken)
                    .UnderlyingCredential;

            var initializer = new ServiceAccountCredential.Initializer(originCredential.Id)
            {
                User = accessToken,
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
