using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Helpers;

public class HttpHostedFileProviderFactory : IHostedFileProviderFactory
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpHostedFileProviderFactory(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public IHostedFileProvider CreateHostedFileProvider()
    {
        var accessToken = _contextAccessor.HttpContext?.Request.Headers["file-access-token"];

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new UnauthorisedException("No file access token was provided.");
        }

        if (Configuration.Configuration.Instance.FileProvider == FileProvider.GoogleDrive)
        {
            var fileProvider = new GoogleFileProvider(accessToken);

            return fileProvider;
        }

        return null;
    }
}