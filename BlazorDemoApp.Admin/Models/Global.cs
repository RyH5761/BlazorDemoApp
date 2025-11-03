namespace BlazorDemoApp.Admin.Models
{
    public class Global
    {
        public string MainTitle { get; set; } = "";
        public string MainTagline { get; set; } = "";
        public string MainDescription { get; set; } = "";
        public string FooterCopyright { get; set; } = "";

        public ChartText Charts { get; set; } = new();
        public ChartText Cards { get; set; } = new();
    }
    
    public class ChartText
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }


}
