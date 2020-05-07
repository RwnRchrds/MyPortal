using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Authorisation.Google;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Google;

namespace MyPortal.Logic.Services
{
    public class GoogleService : BaseService, IGoogleService
    {
        private string[] _scopes;
        private IConfiguration _config;

        public GoogleService(IConfiguration config)
        {
            _config = config;

            _scopes = new[]
            {
                DriveService.Scope.Drive
            };
        }

        public BaseClientService.Initializer GetInitializer()
        {
            var credPath = _config.GetValue<string>("Google:CredentialPath");

            var account = _config.GetValue<string>("Google:AccountName");

            var originCredential =
                (ServiceAccountCredential) GoogleCredential.FromFile(credPath)
                    .UnderlyingCredential;

            var initializer = new ServiceAccountCredential.Initializer(originCredential.Id)
            {
                User = account,
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
