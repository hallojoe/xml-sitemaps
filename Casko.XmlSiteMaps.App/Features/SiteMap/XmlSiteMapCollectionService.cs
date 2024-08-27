using Casko.AspNetCore.XmlSiteMaps;
using Casko.XmlSiteMaps.App.Extensions;
using Casko.AspNetCore.XmlSiteMaps.Enums;
using Casko.AspNetCore.XmlSiteMaps.Models;

namespace Casko.XmlSiteMaps.App.Features.SiteMap;

public class XmlSiteMapCollectionService() : IXmlSiteMapCollection<XmlSiteMap>
{
    public IDictionary<string, string> Routes => new Dictionary<string, string>()
    {
        { "en", "sitemap-collection-default.xml" },
        { "da", "sitemap-collection-da.xml" },
        { "es", "sitemap-collection-es.xml" },
    };

    public XmlSiteMap GetXmlSiteMap(string key, HttpContext httpContext)
    {
        var baseUrl = httpContext.GetBaseUrl();

        var xmlSiteMap = new XmlSiteMap
        {
            Urls = Enumerable.Range(0, 500).Select(number => new XmlSiteMapUrl
            {
                Location = $"{baseUrl}/?page={number}&key={key}",
                LastModified = DateTime.UtcNow.AddDays(-1 * number),
                ChangeFrequency = ChangeFrequency.Weekly,
                Priority = .5
            }).ToList()
        };

        return xmlSiteMap;
    }
}
