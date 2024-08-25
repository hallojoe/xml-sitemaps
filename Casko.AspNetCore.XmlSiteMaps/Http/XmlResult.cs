using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Casko.AspNetCore.XmlSiteMaps.Http;

public class XmlResult<T>(T result) : IResult
{
    private static readonly XmlSerializer Serializer = new(typeof(T));

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        await using var fileBufferingWriteStream = new FileBufferingWriteStream();

        Serializer.Serialize(fileBufferingWriteStream, result);

        httpContext.Response.ContentType = Constants.XmlMimeType;

        await fileBufferingWriteStream.DrainBufferAsync(httpContext.Response.Body);
    }
}