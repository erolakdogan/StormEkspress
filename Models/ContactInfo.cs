namespace StormEkspress.Models
{
    public class ContactInfo
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public SocialLinks SocialLinks { get; set; }
    }
    public class SocialLinks
    {
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Whatsapp { get; set; }
    }
}
