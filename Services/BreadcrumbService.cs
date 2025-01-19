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

            if (currentUrl.StartsWith("/hizmetlerimiz/"))
            {
                breadcrumbs.Add(new Breadcrumb { Text = "Hizmetlerimiz", Url = "/hizmetlerimiz" });
            }

            string pageTitle = GetPageTitle(currentUrl);

            breadcrumbs.Add(new Breadcrumb
            {
                Text = pageTitle,
                Url = currentUrl,
                IsActive = true
            });

            for (int i = 0; i < breadcrumbs.Count; i++)
            {
                if (breadcrumbs[i].Url == currentUrl)
                {
                    breadcrumbs[i].IsActive = true;
                }
                else
                {
                    breadcrumbs[i].IsActive = false;
                }
            }

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
                case "/hizmetlerimiz/paket-basi-kurye":
                    return "Paket Başı Kurye";
                case "/hizmetlerimiz/gun-ici-kurye":
                    return "Gün İçi Kurye";
                case "/hizmetlerimiz/yaya-kurye":
                    return "Yaya Kurye";
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
