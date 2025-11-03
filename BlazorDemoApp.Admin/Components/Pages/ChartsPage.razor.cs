namespace BlazorDemoApp.Admin.Components.Pages
{
    public partial class ChartsPage : ComponentBase
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        
        //private bool IsLoading = true;
        //private ChartsDataSet? chartsData  =  new ();
        
        
        private UiChart? chart1;
        private UiChart? chart2;
        private UiChart? chart3;
        private UiChart? chart4;
        private UiChart? chart5;
        private UiChart? chart6;


        #region _비동기 Json 데이터 로드 
        //protected override async Task OnInitializedAsync()
        //{
        //    await LoadChartDataAsync();
        //    IsLoading = false;
        //}

        /// <summary>
        /// JSON 데이터 비동기 로드
        /// </summary>
        /// <returns></returns>
        //private async Task LoadChartDataAsync()
        //{
        //    //try
        //    //{
        //    //    var baseUri = Nav.BaseUri.TrimEnd('/');
        //    //    var jsonPath = $"{baseUri}/data/chartData.json";

        //    //    chartsData = await Http.GetFromJsonAsync<ChartsDataSet>(jsonPath);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine($"⚠️ chartsData.json 로드 중 실패: {ex.Message}");
        //    //}
        //    //finally
        //    //{
        //     //  IsLoading = false;
        //    //}
        //}
        #endregion

        private async Task RefreshAllCharts()
        {
            //if (IsLoading) return;

            //IsLoading = true;
            //StateHasChanged();

            // 1.Json 데이터 다시 로드
            //await LoadChartDataAsync();

            // 2️.모든 차트 동시 리프레시
            var refreshTasks = new List<Task>();

            //chartsData ??= new ChartsDataSet();
            
            if (chart1 is not null) refreshTasks.Add(chart1.RefreshAsync(App.ChartCommon.Sales));
            if (chart2 is not null) refreshTasks.Add(chart2.RefreshAsync(App.ChartCommon.Market));
            if (chart3 is not null) refreshTasks.Add(chart3.RefreshAsync(App.ChartCommon.Expense));
            if (chart4 is not null) refreshTasks.Add(chart4.RefreshAsync(App.ChartCommon.Bar));
            if (chart5 is not null) refreshTasks.Add(chart5.RefreshAsync(App.ChartCommon.Radar));
            if (chart6 is not null) refreshTasks.Add(chart6.RefreshAsync(App.ChartCommon.Polar));

            // 3️.모든 작업 완료 대기
            await Task.WhenAll(refreshTasks);

            //IsLoading = false;
            //StateHasChanged();

            Console.WriteLine("All charts refreshed successfully.");
        }

        private async Task ReSizeAllCharts()
        {
            //if (IsLoading) return;

           // IsLoading = true;
            //StateHasChanged();

            // 1.Json 데이터 다시 로드
            //await LoadChartDataAsync();

            // 2️.모든 차트 동시 리프레시
            var refreshTasks = new List<Task>();

            //chartsData ??= new ChartsDataSet();

            if (chart1 is not null) refreshTasks.Add(chart1.RefreshAsync(App.ChartCommon.Sales));
            if (chart2 is not null) refreshTasks.Add(chart2.RefreshAsync(App.ChartCommon.Market));
            if (chart3 is not null) refreshTasks.Add(chart3.RefreshAsync(App.ChartCommon.Expense));
            if (chart4 is not null) refreshTasks.Add(chart4.RefreshAsync(App.ChartCommon.Bar));
            if (chart5 is not null) refreshTasks.Add(chart5.RefreshAsync(App.ChartCommon.Radar));
            if (chart6 is not null) refreshTasks.Add(chart6.RefreshAsync(App.ChartCommon.Polar));

            // 3️.모든 작업 완료 대기
            await Task.WhenAll(refreshTasks);

            //IsLoading = false;
            //StateHasChanged();
            Console.WriteLine("All charts refreshed successfully.");
        }
    }
}
