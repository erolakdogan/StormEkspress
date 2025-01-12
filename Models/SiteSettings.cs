namespace StormEkspress.Models
{
    public class SiteSettings
    {
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }
        public string SeoDescription { get; set; }
        public List<string> Keywords { get; set; }
        public string AboutTitle { get; set; }
        public string ProductMenuTitle { get; set; }
        public string ProductMenuSubTitle { get; set; }
        public string FlatProductMenuSubTitle { get; set; }
        public string ContactTitle { get; set; }
        public string ContactSubTitle { get; set; }
        public string FooterCopyright { get; set; }
        public string GoogleMapUrl { get; set; }
    }
}
