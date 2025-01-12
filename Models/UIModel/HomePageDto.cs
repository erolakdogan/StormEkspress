using System.Reflection.PortableExecutable;

namespace StormEkspress.Models.UIModel
{
    public class HomePageDto
    {
        public string CurrentPath { get; set; }
        public SiteSettings SiteSettings { get; set; }
        public List<Intro> Intro { get; set; }
        public List<Menu> Menu { get; set; }
        public About About { get; set; }
        public List<Reference> References { get; set; }
    }
}
