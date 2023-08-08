using System.Threading.Tasks;
using Azure.Core.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MyPortal.Logic.Extensions;

public static class HttpResponseExtensions
{
    public static async Task WriteJsonAsync(this HttpResponse response, object o, string contentType = null)
    {
        var json = JsonConvert.SerializeObject(o);
        await response.WriteJsonAsync(json, contentType);
        await response.Body.FlushAsync();
    }

    public static async Task WriteJsonAsync(this HttpResponse response, string json, string contentType = null)
    {
        response.ContentType = contentType ?? "application/json; charset=UTF-8";
        await response.WriteAsync(json);
        await response.Body.FlushAsync();
    }
}