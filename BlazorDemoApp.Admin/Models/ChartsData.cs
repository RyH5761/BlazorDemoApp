namespace BlazorDemoApp.Admin.Models
{
    public class ChartsDataSet
    {
        /// <summary>
        /// Line Chart Data
        /// </summary>
        public ChartData Sales { get; set; } = new();
        /// <summary>
        /// pie Chart Data
        /// </summary>
        public ChartData Market { get; set; } = new();
        /// <summary>
        /// douthnut Chart Data
        /// </summary>
        public ChartData Expense { get; set; } = new();
        /// <summary>
        /// bar Chart Data
        /// </summary>
        public ChartData Bar { get; set; } = new();
        /// <summary>
        /// radar Chart Data
        /// </summary>
        public ChartData Radar { get; set; } = new();
        /// <summary>
        /// polar Chart Data
        /// </summary>
        public ChartData Polar { get; set; } = new();
    }
    public class ChartData
    {
        public List<string> Labels { get; set; }= new();
        public List<double> Values { get; set; } = new();
    }
}
