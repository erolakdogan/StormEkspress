using StormEkspress.Helper;
using StormEkspress.Services;

var builder = WebApplication.CreateBuilder(args);

// JSON dosyasýný Configuration'a dahil etme
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("wwwroot/localization/tr.json", optional: false, reloadOnChange: true);

// DI container'a hizmetler ekleyin
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new SeoMetaDataFilter());
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<BreadcrumbService>();
// Configuration ayarlarýný ekliyoruz (appsettings.json dosyasýndan)

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller = "Home", action = "About" });

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
