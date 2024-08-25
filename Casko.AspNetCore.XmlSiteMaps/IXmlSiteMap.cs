using Microsoft.AspNetCore.Http;

namespace Casko.AspNetCore.XmlSiteMaps;

public interface IXmlSiteMap<out T> where T : IXmlSiteMapBase
{
    string FileName { get; }
    T GetXmlSiteMap();
}