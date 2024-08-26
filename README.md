# XML Sitemaps

Zero config XML sitemaps. A thing for creating XML sitemaps in AspNetCore applications. 

## Get started

Follow these instructions to get started.

### Install

`dotnet add package Casko.AspNetCore.XmlSiteMaps --version 1.0.0`

### Configure

In `Startup.cs` or `Program.cs`, add XML sitemaps on `IServiceCollection`:

```
services.AddXmlSiteMaps();
```

In `Startup.cs` or `Program.cs`,  use XML sitemaps on `IApplicationBuilder`:

```
app.UseXmlSiteMaps(addRewrites: true); // true is default
```

If ordering creates problems for rewrites then set `useRewrites: false` and then call where it fit in:

```
app.UseXmlSiteMapsRewrites();
```

After configuring XmlSiteMaps, then implementations of `IXmlSiteMap<XmlSiteMapIndex>`, `IXmlSiteMap<XmlSiteMap>`, `IXmlSiteMapCollection<XmlSiteMap>` and `IXmlSiteMapCollection<XmlSiteMapIndex>` will be picked up on application startup and endpoints and rewrites will be setup. 

XML sitemaps will be served from `api/xml-sitemaps/...` and URL rewrites will be created and point `FileName` to it's corresponding endpoint.

### Implement

Example code for creating different XML sitemap things.

#### Create a single XML sitemap:

Simplest for of XML sitemap.

```
public class XmlSiteMapService : IXmlSiteMap<XmlSiteMap>
{
    public string FileName => "sitemap.xml";

    public XmlSiteMap GetXmlSiteMap()
    {
        var xmlSiteMap = new XmlSiteMap();
        
        xmlSiteMap.Urls.Add(new XmlSiteMapUrl()
        {
            Location = "https://...",
            LastModified = DateTime.UtcNow,
            ChangeFrequency = ChangeFrequency.Daily,
            Priority = .5
        });

        ...

        return xmlSiteMap;
    }
}
```

#### Create a XML sitemap index:

When multiple XML sitemaps exist you will need at least one XML sitemap index.

```
public class XmlSiteMapIndexService : IXmlSiteMap<XmlSiteMapIndex>
{
    public string FileName => "sitemap.xml";

    public XmlSiteMapIndex GetXmlSiteMap()
    {
        var xmlSiteMapIndex = new XmlSiteMapIndex();
        
        xmlSiteMap.Locations.Add(new XmlSiteMapIndexLocation()
        {
            Location = "https://...",
        });

        ...

        return xmlSiteMapIndex;
    }
}
```

#### Create XML sitemap collection:

Create many XML sitemaps with a single implementation.

```
public class XmlSiteMapCollectionService() : IXmlSiteMapCollection<XmlSiteMap>
{
    public IDictionary<string, string> Routes => new Dictionary<string, string>()
    {
        { "en", "sitemap-collection-default.xml" },
        { "da", "sitemap-collection-da.xml" },
        { "es", "sitemap-collection-es.xml" },
    };

    public XmlSiteMap GetXmlSiteMap(string key)
    {
        var xmlSiteMap = new XmlSiteMap();

        ...Create things using "key"
        
        xmlSiteMap.Urls.Add(new XmlSiteMapUrl()
        {
            Location = $"https://...{key}",
            LastModified = DateTime.UtcNow,
            ChangeFrequency = ChangeFrequency.Daily,
            Priority = .5
        });
        
        return xmlSiteMap;
    }
}

```


## Changelog

### 1.0.0

 - Initialize project

## TODO

 - Offer automatic XML sitemap index creation when creating `IXmlSitemapCollection<XmlSiteMap>`
 - MAke XHtmlLink not required