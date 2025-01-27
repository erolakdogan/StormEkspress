namespace StormEkspress.Models
{
    public class Feature
    {
        public string? H5 { get; set; }
        public string? P { get; set; }
        public string? Icon { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set; }
        public string Type { get; set; } // "text" veya "image"
        public string? WowDelay { get; set; }
    }
}
