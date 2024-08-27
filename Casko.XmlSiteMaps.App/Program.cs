using Casko.AspNetCore.XmlSiteMaps.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add Xml sitemaps
builder.Services.AddXmlSiteMaps();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Default}/{action=Index}/{id?}");

// Use Xml sitemaps
app.UseXmlSiteMaps(useRewrites: true);

app.Run();