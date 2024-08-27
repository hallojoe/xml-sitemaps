using Microsoft.AspNetCore.Http;

namespace Casko.AspNetCore.XmlSiteMaps;

public interface IXmlSiteMapCollection<out T> where T : IXmlSiteMapBase
{
    /// <summary>
    /// Rewrite routes. One per sitemap. Key will be passed to <see cref="GetXmlSiteMap"/> when called.
    /// Route value can be empty. If so then sitemap will be accessible through controller endpoint.
    /// There is a magic string key that will also create you a sitemap index and include given routes
    /// in that index. This key is named index.
    /// </summary>
    IDictionary<string, string> Routes { get; }
    
    T GetXmlSiteMap(string key, HttpContext httpContext);
    
}