using StormEkspress.Models;

namespace StormEkspress.Services
{
    public class BreadcrumbService
    {
        private readonly IConfiguration _configuration;

        public BreadcrumbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Breadcrumb> GetBreadcrumbs(string currentUrl)
        {
            var breadcrumbs = new List<Breadcrumb>
        {
            new Breadcrumb { Text = "Anasayfa", Url = "/" }
        };
            string pageTitle = GetPageTitle(currentUrl);
            // Aktif sayfa kontrolü: Eğer mevcut URL, menü öğesinin URL'siyle eşleşiyorsa, aktif olarak işaretle
            breadcrumbs.Add(new Breadcrumb
            {
                Text = pageTitle,
                Url = currentUrl,
                IsActive = true
            });

            return breadcrumbs;
        }

        public string GetPageTitle(string currentUrl)
        {
            switch (currentUrl.ToLower())
            {
                case "/hakkimizda":
                    return "Hakkımızda";
                case "/iletisim":
                    return "İletişim";
                case "/hizmetlerimiz":
                    return "Hizmetlerimiz";
                default:
                    return "Sayfa";  // Eğer başka bir sayfa ise "Sayfa" yazdırılır
            }
        }

        // Breadcrumb'ları JSON-LD formatına dönüştüren metod
        public string GetBreadcrumbJson(List<Breadcrumb> breadcrumbs)
        {
            var breadcrumbList = new
            {
                @context = "https://schema.org",
                @type = "BreadcrumbList",
                itemListElement = breadcrumbs.Select((breadcrumb, index) => new
                {
                    @type = "ListItem",
                    position = index + 1,
                    name = breadcrumb.Text,
                    item = breadcrumb.Url
                }).ToList()
            };

            // JSON-LD formatındaki breadcrumb verisini döndür
            return Newtonsoft.Json.JsonConvert.SerializeObject(breadcrumbList);
        }
    }
}
