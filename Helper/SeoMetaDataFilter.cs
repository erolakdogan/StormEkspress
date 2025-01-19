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
                case "/":
                    return "Anasayfa | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hakkimizda":
                    return "Hakkımızda | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/iletisim":
                    return "İletişim | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hizmetlerimiz":
                    return "Hizmetlerimiz | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hizmetlerimiz/paket-basi-kurye":
                    return "Paket Başı Kurye | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hizmetlerimiz/gun-ici-kurye":
                    return "Gün İçi Kurye | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                case "/hizmetlerimiz/yaya-kurye":
                    return "Yaya Kurye | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
                default:
                    return "Sayfa | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti";
            }
        }

        private string GetPageDescription(string url)
        {
            switch (url.ToLower())
            {
                case "/":
                    return "Anasayfa | Karabük'te Hızlı ve Güvenilir Kurye Hizmeti websitemize hoşgeldiniz.";
                case "/hakkimizda":
                    return "Hakkımızda sayfası, Karabük, Safranbolu ve çevresindeki restoranlar ve kafelere sunduğumuz hızlı kurye hizmetini tanıtıyor.";
                case "/iletisim":
                    return "İletişim sayfası ile bizimle kolayca iletişim kurabilirsiniz.";
                case "/hizmetlerimiz":
                    return "Hizmetlerimiz sayfası,  Karabük, Safranbolu ve çevresindeki restoranlar ve kafelere sunduğumuz hizmetlerin detaylarını içeriyor.";
                case "/hizmetlerimiz/paket-basi-kurye":
                    return "Paket Başı Kurye, küçük ve acil paketlerin hızlı teslimatı için ideal bir çözümdür.";
                case "/hizmetlerimiz/gun-ici-kurye":
                    return "Gün İçi Kurye, aynı gün içinde teslimat yaparak hızlı ve güvenilir hizmet sunar.";
                case "/hizmetlerimiz/yaya-kurye":
                    return "Yaya Kurye, şehir içindeki teslimatlar için çevreci ve hızlı bir alternatiftir.";
                default:
                    return "Karabük, Safranbolu ve çevresindeki çevresindeki restoranlar ve kafelere sunduğumuz kurye hizmetini keşfedin.";
            }
        }
    }
}
