using Casko.AspNetCore.XmlSiteMaps;
using Casko.XmlSiteMaps.App.Extensions;
using Casko.AspNetCore.XmlSiteMaps.Models;

namespace Casko.XmlSiteMaps.App.Features.SiteMap;

public class XmlSiteMapIndexService(IHttpContextAccessor httpContextAccessor) : IXmlSiteMap<XmlSiteMapIndex>
{
    public string FileName => "sitemap-index.xml";

    public XmlSiteMapIndex GetXmlSiteMap()
    {
        var baseUrl = httpContextAccessor.HttpContext.GetBaseUrl();
        
        var xmlSiteMap = new XmlSiteMapIndex
        {
            Locations =
            [
                new()
                {
                    Location = $"{baseUrl}/sitemap-default.xml"
                }
            ]
        };

        return xmlSiteMap;
    }
}