using System.Xml.Serialization;

namespace Casko.AspNetCore.XmlSiteMaps.Models;

/// <summary>
/// Represents the root element of an XML sitemap index, which references multiple sitemaps.
/// This class is used to serialize and deserialize the sitemap index, allowing a website to provide multiple sitemap files, each containing its own set of URLs.
/// </summary>
[XmlRoot(Constants.SiteMapIndexElement, Namespace = Constants.Namespace)]
public sealed class XmlSiteMapIndex : IXmlSiteMapBase
{
    /// <summary>
    /// Gets or sets the list of sitemap locations included in the sitemap index.
    /// Each location is represented by an instance of the <see cref="XmlSiteMapIndexLocation"/> class, which specifies the URL of an individual sitemap.
    /// </summary>
    [XmlElement(Constants.SiteMapElement, Type = typeof(XmlSiteMapIndexLocation))]
    public required List<XmlSiteMapIndexLocation> Locations { get; set; }
}