using System.Xml.Serialization;

namespace Casko.AspNetCore.XmlSiteMaps.Models;

/// <summary>
/// Represents the root element of an XML sitemap, containing a collection of URLs.
/// This class is used to serialize and deserialize the sitemap, which helps search engines understand the structure of a website.
/// </summary>
[XmlRoot(Constants.UrlSetElement, Namespace = Constants.Namespace)]
public sealed class XmlSiteMap : IXmlSiteMapBase
{
    /// <summary>
    /// Gets or sets the list of URLs included in the sitemap.
    /// Each URL is represented by an instance of the <see cref="XmlSiteMapUrl"/> class, which contains the details of a single page.
    /// </summary>
    [XmlElement(Constants.UrlElement, Type = typeof(XmlSiteMapUrl))]
    public List<XmlSiteMapUrl> Urls { get; set; } = new();
}