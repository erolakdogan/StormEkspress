using StormEkspress.Helper;
using StormEkspress.Services;
using StormEkspress.Services.Implementations;
using StormEkspress.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// JSON dosyasýný Configuration'a dahil etme
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("wwwroot/localization/tr.json", optional: false, reloadOnChange: true);

// DI container'a hizmetler ekleyin
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new SeoMetaDataFilter());
});
builder.Services.AddSingleton<BreadcrumbService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddHttpClient();
// Configuration ayarlarýný ekliyoruz (appsettings.json dosyasýndan)

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

app.Run();
