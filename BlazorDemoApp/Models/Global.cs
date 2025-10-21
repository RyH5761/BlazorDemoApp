namespace BlazorDemoApp.Models
{
    public class Global
    {
        public string MainTitle { get; set; } = "";
        public string MainTagline { get; set; } = "";
        public string MainDescription { get; set; } = "";
        public string FooterCopyright { get; set; } = "";

        public List<FeatureItem> Features { get; set; } = new();

        public AboutText About { get; set; } = new();
        public ContactText Contact { get; set; } = new();
        public PricingText Pricing { get; set; } = new();
        public FAQText FAQ { get; set; } = new();
    }
    public class FeatureItem
    {
        public string Icon { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }
    public class AboutText
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class ContactText
    {
        public string Title { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }

    public class PricingText
    {
        public string Title { get; set; } = "";
        public string Basic { get; set; } = "";
        public string Pro { get; set; } = "";
        public string Enterprise { get; set; } = "";
    }

    public class FAQText
    {
        public string Title { get; set; } = "";
        public string Intro { get; set; } = "";
    }

}
