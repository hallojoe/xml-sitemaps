using Casko.AspNetCore.XmlSiteMaps;
using Casko.XmlSiteMaps.App.Extensions;
using Casko.AspNetCore.XmlSiteMaps.Enums;
using Casko.AspNetCore.XmlSiteMaps.Models;

namespace Casko.XmlSiteMaps.App.Features.SiteMap;

public class XmlSiteMapService(IHttpContextAccessor httpContextAccessor) : IXmlSiteMap<XmlSiteMap>
{
    public string FileName => "sitemap-simple.xml";

    public XmlSiteMap GetXmlSiteMap()
    {

        var baseUrl = httpContextAccessor.HttpContext.GetBaseUrl();

        var xmlSiteMap = new XmlSiteMap
        {
            Urls = Enumerable.Range(0, 500).Select(number => new XmlSiteMapUrl
            {
                Location = $"{baseUrl}/?page={number}&filename={FileName}",
                LastModified = DateTime.UtcNow.AddDays(-1 * number),
                ChangeFrequency = ChangeFrequency.Weekly,
                Priority = .5
            }).ToList()
        };

        return xmlSiteMap;
    }
}
