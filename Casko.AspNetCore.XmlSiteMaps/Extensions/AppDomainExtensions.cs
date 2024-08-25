using System.Reflection;

namespace Casko.AspNetCore.XmlSiteMaps.Extensions;

internal static class AppDomainExtensions
{
    internal static List<Type?> GetServiceTypes(this AppDomain appDomain, Type serviceType)
    {
        return appDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException reflectionTypeLoadException)
                {
                    return reflectionTypeLoadException.Types.Where(type => type != null);
                }
                catch (Exception)
                {
                    return [];
                }
            })
            .Where(type => type is { IsClass: true, IsAbstract: false })
            .Select(type => new
            {
                Type = type,
                Interface = type?.GetInterfaces()
                    .FirstOrDefault(innerType => innerType.IsGenericType &&
                                                 innerType.GetGenericTypeDefinition() == serviceType &&
                                                 typeof(IXmlSiteMapBase).IsAssignableFrom(innerType
                                                     .GetGenericArguments().First()))
            })
            .Where(typeInterfaceTuple => typeInterfaceTuple.Interface != null)
            .Select(typeInterfaceTuple => typeInterfaceTuple.Type)
            .ToList();
    }
}