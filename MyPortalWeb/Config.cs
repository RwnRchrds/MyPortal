using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MyPortalWeb
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("myportal", "MyPortal API")
            };
        
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "7A0B5192-44D5-4514-9418-DE4AF7974182",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "myportal" },
                    RequireClientSecret = false,
                    ClientName = "MyPortal Web App",
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                }
            };
    }
}