using System.Xml.Serialization;
using Casko.AspNetCore.XmlSiteMaps.Attributes;
using Casko.AspNetCore.XmlSiteMaps.Enums;

namespace Casko.AspNetCore.XmlSiteMaps.Models;

/// <summary>
/// Represents a single URL entry in an XML sitemap.
/// This class is used to serialize and deserialize individual URLs, along with their metadata, such as last modified date, change frequency, priority, and any alternate language versions.
/// </summary>
public sealed class XmlSiteMapUrl
{
    private double? _priority;

    /// <summary>
    /// Gets or sets the URL of the page.
    /// This is the primary location that search engines will index.
    /// </summary>
    [XmlElement(Constants.LocationElement)]
    public required string Location { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the page was last modified.
    /// This value is ignored during XML serialization, but it can be accessed and modified programmatically.
    /// </summary>
    [XmlIgnore]
    public required DateTime LastModified { get; set; }

    /// <summary>
    /// Gets or sets the last modified date as a formatted string for XML serialization.
    /// The date is formatted according to a predefined pattern and is used during XML serialization.
    /// </summary>
    [XmlElement(Constants.LastModifiedElement)]
    public string LastModifiedFormatted
    {
        get => LastModified.ToString(Constants.DateFormat);
        set => LastModified = DateTime.TryParse(value, out var date) ? date : default;
    }

    /// <summary>
    /// Gets or sets the change frequency of the URL, which indicates how frequently the content at this URL is likely to change.
    /// This value is ignored during XML serialization but can be accessed and modified programmatically.
    /// </summary>
    [XmlIgnore]
    public ChangeFrequency ChangeFrequency { get; set; }

    /// <summary>
    /// Gets or sets the change frequency as a string for XML serialization.
    /// If the change frequency is set to None, this element will be omitted in the XML.
    /// </summary>
    [XmlElement(Constants.ChangeFrequencyElement)]
    public string? ChangeFrequencySerialized
    {
        get => ChangeFrequency == ChangeFrequency.None ? null : ChangeFrequency.ToString().ToLowerInvariant();
        set => ChangeFrequency =
            string.IsNullOrEmpty(value) ? ChangeFrequency.None : Enum.Parse<ChangeFrequency>(value);
    }

    /// <summary>
    /// Gets or sets the priority of the URL relative to other URLs on the site.
    /// The priority is a decimal value between 0.1 and 1.0, rounded to one decimal place.
    /// This property also includes validation to ensure the value falls within the allowed range.
    /// </summary>
    [PriorityValidation]
    [XmlElement(Constants.PriorityElement)]
    public double? Priority
    {
        get => _priority;
        set
        {
            if (value.HasValue)
            {
                var roundedValue = Math.Round(value.Value, 1);
                if (roundedValue < 0.1d || roundedValue > 1.0d || roundedValue * 10 % 1 != 0) _priority = null;
                _priority = roundedValue;
            }
            else
            {
                _priority = null;
            }
        }
    }

    /// <summary>
    /// Gets or sets a list of alternate versions of the URL in different languages or regions.
    /// Each link is represented by an instance of the <see cref="XHtmlLink"/> class.
    /// </summary>
    [XmlElement(Type = typeof(XHtmlLink), ElementName = Constants.XhtmlLinkElement, Namespace = Constants.XhtmlLinkNamespace)]
    public List<XHtmlLink>? CultureLinks { get; set; }
}
