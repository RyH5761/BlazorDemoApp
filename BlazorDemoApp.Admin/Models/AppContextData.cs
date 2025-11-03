namespace BlazorDemoApp.Admin.Models
{
    public class AppContextData
    {
        public Global Common { get; set; } = new();
        public ChartsDataSet ChartCommon { get; set; } = new();
    }
}
