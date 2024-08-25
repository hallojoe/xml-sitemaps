using Microsoft.AspNetCore.Mvc;
using Casko.AspNetCore.XmlSiteMaps.Services;

namespace Casko.XmlSiteMaps.App.Controllers;

public class DefaultController(IXmlSiteMapRouteService xmlSiteMapRouteService) : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "XmlSiteMaps";
        ViewData["Routes"] = xmlSiteMapRouteService.GetRoutes();
            
        return View();
    }
}