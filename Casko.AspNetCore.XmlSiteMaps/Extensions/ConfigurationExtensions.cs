using System.Text.RegularExpressions;
using Casko.AspNetCore.XmlSiteMaps.Http;
using Casko.AspNetCore.XmlSiteMaps.Models;
using Casko.AspNetCore.XmlSiteMaps.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;

namespace Casko.AspNetCore.XmlSiteMaps.Extensions;

public static class ConfigurationExtensions
{
    private static void AddXmlSiteMapServiceTypes(this IServiceCollection services, Type serviceType)
    {
        var xmlSiteMapServiceTypes = AppDomain.CurrentDomain.GetServiceTypes(serviceType);

        foreach (var xmlSiteMapServiceType in xmlSiteMapServiceTypes)
        {
            var serviceInterface = xmlSiteMapServiceType?.GetInterfaces()
                .FirstOrDefault(type => type.IsGenericType && type.GetGenericTypeDefinition() == serviceType);

            if (serviceInterface != null && xmlSiteMapServiceType != null)
                services.AddTransient(serviceInterface, xmlSiteMapServiceType);
        }
    }

    private static string CreateEndPointRoute(string fileName)
    {
        var fileNameWithoutExtension = fileName[..fileName.LastIndexOf('.')];

        var result = $"{Constants.RouteBase}/{fileNameWithoutExtension}";

        return result;
    }

    private static void CreateEndPoints<T>(this IApplicationBuilder applicationBuilder,
        IXmlSiteMapRouteService xmlSiteMapRouteService) where T : IXmlSiteMapBase
    {
        var xmlSiteMapModelServices = 
            applicationBuilder.ApplicationServices.GetServices<IXmlSiteMap<T>>();
        
        applicationBuilder.UseEndpoints(endPoints =>
        {
            foreach (var xmlSiteMapModelService in xmlSiteMapModelServices)
            {
                var route = CreateEndPointRoute(xmlSiteMapModelService.FileName);

                xmlSiteMapRouteService.RegisterRoute(route, xmlSiteMapModelService.FileName);

                endPoints.MapGet(route, () => new XmlResult<T>(xmlSiteMapModelService.GetXmlSiteMap()));

            }

            var multilingualXmlSiteMapModelServices =
                applicationBuilder.ApplicationServices.GetServices<IXmlSiteMapCollection<T>>();

            foreach (var multilingualXmlSiteMapModelService in multilingualXmlSiteMapModelServices)
            {
                foreach (var cultureKey in multilingualXmlSiteMapModelService.Routes.Keys)
                {
                    var fileNameForCulture = multilingualXmlSiteMapModelService.Routes[cultureKey];

                    var route = CreateEndPointRoute(fileNameForCulture);

                    xmlSiteMapRouteService.RegisterRoute(route, fileNameForCulture);

                    endPoints.MapGet(route, () => new XmlResult<T>(multilingualXmlSiteMapModelService.GetXmlSiteMap(cultureKey)));
                }                
            }
        });
    }

    private static void CreateRewrites<T>(this IApplicationBuilder applicationBuilder,
        IXmlSiteMapRouteService xmlSiteMapRouteService) where T : IXmlSiteMapBase
    {
        var xmlSiteMapModelServices = applicationBuilder.ApplicationServices.GetServices<IXmlSiteMap<T>>().ToList();

        foreach (var xmlSiteMapModelService in xmlSiteMapModelServices)
        {
            var route = CreateEndPointRoute(xmlSiteMapModelService.FileName);

            xmlSiteMapRouteService.RegisterRoute(route, xmlSiteMapModelService.FileName);
        }

        var multilingualXmlSiteMapModelServices =
            applicationBuilder.ApplicationServices.GetServices<IXmlSiteMapCollection<T>>().ToList();

        foreach (var multilingualXmlSiteMapModelService in multilingualXmlSiteMapModelServices)
        foreach (var cultureKey in multilingualXmlSiteMapModelService.Routes.Keys)
        {
            var fileNameForCulture = multilingualXmlSiteMapModelService.Routes[cultureKey];

            var route = CreateEndPointRoute(fileNameForCulture);

            xmlSiteMapRouteService.RegisterRoute(route, fileNameForCulture);
        }


        var rewriteOptions = new RewriteOptions();

        foreach (var xmlSiteMapModelService in xmlSiteMapModelServices)
        {
            var route = CreateEndPointRoute(xmlSiteMapModelService.FileName);

            var pattern = $"^{Regex.Escape(xmlSiteMapModelService.FileName)}";

            rewriteOptions.AddRewrite(pattern, route, true);
        }

        foreach (var multilingualXmlSiteMapModelService in multilingualXmlSiteMapModelServices)
        foreach (var cultureKey in multilingualXmlSiteMapModelService.Routes.Keys)
        {
            var fileNameForCulture = multilingualXmlSiteMapModelService.Routes[cultureKey];

            var route = CreateEndPointRoute(fileNameForCulture);

            var pattern = $"^{Regex.Escape(fileNameForCulture)}";

            rewriteOptions.AddRewrite(pattern, route, true);
        }

        applicationBuilder.UseRewriter(rewriteOptions);
    }

    public static IServiceCollection AddXmlSiteMaps(this IServiceCollection? services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IXmlSiteMapRouteService, XmlSiteMapRouteService>();

        services.AddXmlSiteMapServiceTypes(typeof(IXmlSiteMap<>));

        services.AddXmlSiteMapServiceTypes(typeof(IXmlSiteMapCollection<>));

        return services;
    }

    public static IApplicationBuilder UseXmlSiteMaps(this IApplicationBuilder applicationBuilder, bool useRewrites = false)
    {
        ArgumentNullException.ThrowIfNull(applicationBuilder);

        var routeService = applicationBuilder.ApplicationServices.GetRequiredService<IXmlSiteMapRouteService>();

        applicationBuilder.CreateEndPoints<XmlSiteMap>(routeService);

        applicationBuilder.CreateEndPoints<XmlSiteMapIndex>(routeService);

        if (useRewrites) applicationBuilder.UseXmlSiteMapsRewrites();
        
        return applicationBuilder;
    }

    public static IApplicationBuilder UseXmlSiteMapsRewrites(this IApplicationBuilder? applicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(applicationBuilder);

        var routeService = applicationBuilder.ApplicationServices.GetRequiredService<IXmlSiteMapRouteService>();

        applicationBuilder.CreateRewrites<XmlSiteMap>(routeService);

        applicationBuilder.CreateRewrites<XmlSiteMapIndex>(routeService);

        return applicationBuilder;
    }
    
}