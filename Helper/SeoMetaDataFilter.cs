using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace StormEkspress.Helper
{
    public class SeoMetaDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                // URL'yi dinamik olarak alıyoruz
                var url = context.HttpContext.Request.Path.ToString();

                // Dinamik olarak title, description, og:url gibi SEO bilgilerini ViewData'ya ekliyoruz
                controller.ViewData["Title"] = GetPageTitle(url); // Sayfa başlığı
                controller.ViewData["Description"] = GetPageDescription(url); // Sayfa açıklaması
                controller.ViewData["OGUrl"] = context.HttpContext.Request.Scheme + "://" + context.HttpContext.Request.Host + url; // Dinamik og:url
                controller.ViewData["OGImage"] = "/assets/img/logo.webp"; // OG Image (görsel)
            }

            base.OnActionExecuting(context); // Filtrenin çalışmasını sağlıyoruz
        }

        // Sayfa başlıklarını URL'ye göre dinamik olarak döndüren metod
        private string GetPageTitle(string url)
        {
            switch (url.ToLower())
            {
                case "/hakkimizda":
                    return "Hakkımızda | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/iletisim":
                    return "İletişim | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hizmetlerimiz":
                    return "Hizmetlerimiz | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                default:
                    return "Sayfa | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
            }
        }

        private string GetPageDescription(string url)
        {
            switch (url.ToLower())
            {
                case "/hakkimizda":
                    return "Hakkımızda sayfası, Karabük, Safranbolu ve çevresindeki restoranlar ve kafelere sunduğumuz hızlı kurye hizmetini tanıtıyor.";
                case "/iletisim":
                    return "İletişim sayfası ile bizimle kolayca iletişim kurabilirsiniz.";
                case "/hizmetlerimiz":
                    return "Hizmetlerimiz sayfası,  Karabük, Safranbolu ve çevresindeki restoranlar ve kafelere sunduğumuz hizmetlerin detaylarını içeriyor.";
                default:
                    return "Karabük, Safranbolu ve çevresindeki çevresindeki restoranlar ve kafelere sunduğumuz kurye hizmetini keşfedin.";
            }
        }
    }
}
