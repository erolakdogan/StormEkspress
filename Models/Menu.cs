namespace StormEkspress.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Menu> SubMenu { get; set; }
    }
}
