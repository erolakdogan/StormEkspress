namespace StormEkspress.Models
{
    public class Statistic
    {
        public string Title { get; set; }
        public int Value { get; set; }
        public string Icon { get; set; }
        public string Suffix { get; set; }
        public string BgColor { get; set; } // Arka plan rengi için
        public double Delay { get; set; } // Animasyon gecikmesi
    }
}
