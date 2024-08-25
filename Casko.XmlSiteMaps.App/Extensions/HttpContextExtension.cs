namespace Casko.XmlSiteMaps.App.Extensions;

internal static class HttpContextExtension
{
    internal static string GetBaseUrl(this HttpContext? httpContext)
    {
        var httpRequest = httpContext?.Request ?? throw new NullReferenceException(nameof(HttpRequest));

        var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";

        return baseUrl;
    }
}