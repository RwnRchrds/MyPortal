using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Drive.v3;
using Microsoft.Extensions.Configuration;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.DocumentProviders
{
    public class GoogleDocumentProvider : IDocumentProvider
    {
        private readonly DriveService _driveService;

        public GoogleDocumentProvider(IConfiguration config)
        {
            var googleHelper = new GoogleHelper(config);
            _driveService = new DriveService(googleHelper.GetInitializer());
        }
    }
}
