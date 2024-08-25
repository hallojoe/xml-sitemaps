using System.Xml.Serialization;

namespace Casko.AspNetCore.XmlSiteMaps.Models;

/// <summary>
/// Represents an XHTML link element within an XML sitemap.
/// This class is used to define relationships between different versions of a URL, such as language-specific variants.
/// </summary>
public sealed class XHtmlLink
{
    /// <summary>
    /// Gets or sets the relationship type of the linked URL.
    /// Typically, this is set to "alternate" to indicate an alternative version of the content, such as a different language or regional version.
    /// </summary>
    [XmlAttribute(AttributeName = Constants.Rel, Namespace = Constants.Empty)]
    public required string Rel { get; set; } = Constants.RelAlternate;

    /// <summary>
    /// Gets or sets the language or region code of the linked content.
    /// This should follow the RFC 5646 format, such as "en" for English or "es" for Spanish.
    /// </summary>
    [XmlAttribute(AttributeName = Constants.HrefLang, Namespace = Constants.Empty)]
    public required string HrefLang { get; set; }

    /// <summary>
    /// Gets or sets the URL of the alternate version of the content.
    /// This URL should point to the location of the content in the specified language or region.
    /// </summary>
    [XmlAttribute(AttributeName = Constants.Href, Namespace = Constants.Empty)]
    public required string Href { get; set; }
}