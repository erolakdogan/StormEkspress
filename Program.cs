using StormEkspress.Helper;
using StormEkspress.Services;
using StormEkspress.Services.Implementations;
using StormEkspress.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("wwwroot/localization/tr.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("wwwroot/localization/keywords.json", optional: false, reloadOnChange: true);
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new SeoMetaDataFilter());
});
builder.Services.AddSingleton<BreadcrumbService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "courierApplicationForm",
    pattern: "basvuru/kurye",
    defaults: new { controller = "Home", action = "CourierApplicationForm" });

app.MapControllerRoute(
    name: "restaurantApplicationForm",
    pattern: "basvuru/restoran",
    defaults: new { controller = "Home", action = "RestaurantApplicationForm" });

app.MapControllerRoute(
    name: "services",
    pattern: "hizmetlerimiz",
    defaults: new { controller = "Home", action = "Services" });

app.MapControllerRoute(
    name: "serviceDetail",
    pattern: "hizmetlerimiz/{slug}",
    defaults: new { controller = "Home", action = "ServiceDetail" });

app.MapControllerRoute(
    name: "contact",
    pattern: "iletisim",
    defaults: new { controller = "Home", action = "Contact" });

app.MapControllerRoute(
    name: "sitemap",
    pattern: "sitemap.xml",
    defaults: new { controller = "Home", action = "GenerateSitemap" });

app.Run();
