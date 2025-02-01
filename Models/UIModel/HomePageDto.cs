using StormEkspress.Models.Entities;
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
        public List<Service> Services { get; set; }
        public Service Service { get; set; }
        public CourierApplication CourierApplicationForm { get; set; }
        public RestaurantApplication RestaurantApplicationForm { get; set; }
        public List<Feature> Features { get; set; }
        public FormInfo FormInfos { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Statistic> Statistics { get; set; }
        public List<ServiceDetail> ServiceDetails { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<string> Keywords { get; set; }
    }
}
