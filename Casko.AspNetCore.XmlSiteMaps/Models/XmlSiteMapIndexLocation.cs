using System.Xml.Serialization;

namespace Casko.AspNetCore.XmlSiteMaps.Models;

/// <summary>
/// Represents a single sitemap location within an XML sitemap index.
/// This class is used to specify the URL of an individual sitemap file that is part of a sitemap index.
/// </summary>
public sealed class XmlSiteMapIndexLocation
{
    /// <summary>
    /// Gets or sets the URL of the sitemap file referenced by the sitemap index.
    /// This URL points to the location of a sitemap that contains a set of URLs for the website.
    /// </summary>
    [XmlElement(Constants.LocationElement)]
    public required string Location { get; set; }
}