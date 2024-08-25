namespace Casko.AspNetCore.XmlSiteMaps.Services;

public interface IXmlSiteMapRouteService
{
    void RegisterRoute(string controllerRoute, string rewriteRoute);
    Dictionary<string, string> GetRoutes();
}