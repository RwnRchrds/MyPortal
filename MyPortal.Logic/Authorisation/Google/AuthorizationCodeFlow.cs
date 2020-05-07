using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;

namespace MyPortal.Logic.Authorisation.Google
{
    public class AuthorizationCodeFlow : GoogleAuthorizationCodeFlow
    {
        public AuthorizationCodeFlow(Initializer initializer) : base(initializer)
        {
            
        }

        public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
        {
            return new GoogleAuthorizationCodeRequestUrl(new Uri(AuthorizationServerUrl))
            {
                ClientId = ClientSecrets.ClientId,
                Scope = string.Join(" ", Scopes),
                RedirectUri = WebAuthorizationBroker.RedirectUri,
                AccessType = "offline",
                Prompt = "consent"
            };
        }
    }
}
