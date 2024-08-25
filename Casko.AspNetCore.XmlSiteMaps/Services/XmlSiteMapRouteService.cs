namespace Casko.AspNetCore.XmlSiteMaps.Services;

public sealed class XmlSiteMapRouteService : IXmlSiteMapRouteService
{
    private readonly Dictionary<string, string> _routes = new();

    public void RegisterRoute(string controllerRoute, string rewriteRoute)
    {
        _routes.TryAdd(controllerRoute, rewriteRoute);
    }

    public Dictionary<string, string> GetRoutes()
    {
        return _routes;
    }
}