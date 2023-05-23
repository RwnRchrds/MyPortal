using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MyPortal.Logic.Configuration;
using MyPortalWeb.Attributes;
using MyPortalWeb.Models.Response;

namespace MyPortalWeb.Middleware;

public class FileProviderMiddleware
{
    private RequestDelegate _next;

    public FileProviderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Endpoint endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        FileProviderAttribute attribute = endpoint?.Metadata.GetMetadata<FileProviderAttribute>();

        if (attribute != null)
        {
            if (attribute.FileProviders.Contains(Configuration.Instance.FileProvider))
            {
                await _next(context);
                return;
            }
            
            context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            var error = new ErrorResponseModel(
                "The file provider required to complete this request is not configured.");
            await context.Response.WriteJsonAsync(error);
            context.Response.ContentLength = context.Response.Body.Length;
            await context.Response.CompleteAsync();
        }
    }
}