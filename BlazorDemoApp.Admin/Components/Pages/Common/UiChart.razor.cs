namespace BlazorDemoApp.Admin.Components.Pages.Common
{
    public partial class UiChart : ComponentBase
    {
        [Parameter] public ChartTypes ChartType { get; set; } = ChartTypes.Line;
        //[Parameter] public string ChartTitle { get; set; } = "Untitled Chart";
        [Parameter] public string ChartClass { get; set; } = "chart-medium";
        [Parameter] public string HeaderColor { get; set; } = "#4e73df";
        [Parameter] public ChartData? chartData { get; set; }

        // 선택: 외부에서 필요 시 오버라이드 (기본은 CSS로 처리)
        [Parameter] public string MaxWidth { get; set; } = "100%";
        [Parameter] public string Height { get; set; } = "auto";

        private bool isReady;
        // 차트 실제 인스턴스(ref)
        private BaseChart<double>? chartRef;
        // 재마운트 트리거용 키(@key)
        private object _renderKey = Guid.NewGuid();

        /// <summary>
        /// 랜더링 후 초기화
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // 1. 차트 렌더용 플래그 true (차트 마크업 먼저 그리기)
                isReady = false;
                await Task.Delay(500);
                isReady = true;
                StateHasChanged();

                // 2. 렌더 완료 후 chartRef 연결 기다림
                await WaitForChartReference();

                // 3. 실제 데이터 초기화
                await InitializeChartAsync();

              //  _previousSizeClass = ChartClass;
            }
            //else if (_previousSizeClass != ChartClass)
            //{
            //    _previousSizeClass = ChartClass;
            //    await TriggerChartResizeAsync();// 크기 변경 시 재마운트 + 재초기화
            //}
        }

        /// <summary>
        /// 차트타입에 따른 렌더링처리
        /// </summary>
        /// <LineChart TItem="double" @ref="lineChartData"/>
        /// <returns></returns>
        private RenderFragment RenderChart() => builder =>
        {
            var componentType = ChartType switch
            {
                ChartTypes.Line      => typeof(LineChart<double>),
                ChartTypes.Pie       => typeof(PieChart<double>),
                ChartTypes.Doughnut  => typeof(DoughnutChart<double>),
                ChartTypes.Bar       => typeof(BarChart<double>),
                ChartTypes.Radar     => typeof(RadarChart<double>),
                ChartTypes.Polar     => typeof(PolarAreaChart<double>),
                _ => typeof(LineChart<double>)
            };

            builder.OpenComponent(0, componentType);
            builder.SetKey(_renderKey);                // <-- (재마운트를 위한  키 설정)
            builder.AddAttribute(1, "class", "w-100 h-100");
            builder.AddComponentReferenceCapture(2, inst => chartRef = (BaseChart<double>)inst);
            builder.CloseComponent();
        };
        /// <summary>
        /// chartRef가 실제 렌더링 후 JS 쪽에서 연결되기까지 기다리는 함수 
        /// </summary>
        /// <returns></returns>
        private async Task WaitForChartReference()
        {
            int retry = 0;
            while (chartRef == null && retry < 20)
            {
                await Task.Delay(100);
                retry++;
            }
        }
        /// <summary>
        /// 차트 기본설정 처리
        /// </summary>
        /// <returns></returns>
        private async Task InitializeChartAsync()
        {
            if (chartData == null || chartData.Values.Count == 0)
                return;
            if (chartRef == null)
                return;

            var colors = new List<string> { "#4e73df", "#1cc88a", "#36b9cc", "#f6c23e" };

            switch (ChartType)
            {
                case ChartTypes.Line when chartRef is LineChart<double> lineChart:
                    await lineChart.Clear();
                    await lineChart.AddLabels(chartData.Labels.ToArray());
                    await lineChart.AddDataSet(new LineChartDataset<double>
                    {
                        Label           = "Sales",
                        Data            = chartData.Values,
                        BorderColor     = new List<string> { HeaderColor },
                        BackgroundColor = new List<string> { "rgba(78,115,223,0.2)" },
                        Fill = true,
                        Tension = 0.4f
                    });
                    await lineChart.SetOptions(new LineChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await lineChart.Update();
                    break;

                case ChartTypes.Pie when chartRef is PieChart<double> pieChart:
                    await pieChart.Clear();
                    await pieChart.AddLabels(chartData.Labels.ToArray());
                    await pieChart.AddDataSet(new PieChartDataset<double>
                    {
                        Data            = chartData.Values,
                        BackgroundColor = colors
                    });
                    await pieChart.SetOptions(new PieChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await pieChart.Update();
                    break;

                case ChartTypes.Bar when chartRef is BarChart<double> barChart:
                    await barChart.Clear();
                    await barChart.AddLabels(chartData.Labels.ToArray());
                    await barChart.AddDataSet(new BarChartDataset<double>
                    {
                        Label           = "Revenue",
                        Data            = chartData.Values,
                        BackgroundColor = colors
                    });
                    await barChart.SetOptions(new BarChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await barChart.Update();
                    break;

                case ChartTypes.Doughnut when chartRef is DoughnutChart<double> doughnutChart:
                    await doughnutChart.Clear();
                    await doughnutChart.AddLabels(chartData.Labels.ToArray());
                    await doughnutChart.AddDataSet(new DoughnutChartDataset<double>
                    {
                        Data            = chartData.Values,
                        BackgroundColor = colors
                    });
                    await doughnutChart.SetOptions(new DoughnutChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await doughnutChart.Update();
                    break;

                case ChartTypes.Radar when chartRef is RadarChart<double> radarChart:
                    await radarChart.Clear();
                    await radarChart.AddLabels(chartData.Labels.ToArray());
                    await radarChart.AddDataSet(new RadarChartDataset<double>
                    {
                        Label = "Rader",
                        Data = chartData.Values,
                        BorderColor = new List<string> { HeaderColor },
                        BackgroundColor = new List<string> { "rgba(78,115,223,0.2)" }
                    });
                    await radarChart.SetOptions(new RadarChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await radarChart.Update();
                    break;

                case ChartTypes.Polar when chartRef is PolarAreaChart<double> polarChart:
                    await polarChart.Clear();
                    await polarChart.AddLabels(chartData.Labels.ToArray());
                    await polarChart.AddDataSet(new PolarAreaChartDataset<double>
                    {
                        Data            = chartData.Values,
                        BackgroundColor = colors
                    });
                    await polarChart.SetOptions(new PolarAreaChartOptions
                    {
                        Responsive = true,
                        MaintainAspectRatio = false
                    });
                    await polarChart.Update();
                    break;

              
            }

        }

        /// <summary>
        /// 컨테이너 크기변경에 따른 차트 리사이즈 트리거
        /// </summary>
        /// <returns></returns>
        private async Task TriggerChartResizeAsync()
        {
            try
            {
                //1. 재마운트를 위한 키 변경
                _renderKey = Guid.NewGuid();
                chartRef = null;
                StateHasChanged();

                // 2) ref 확보 후 재초기화
                await WaitForChartReference();
                await InitializeChartAsync();

                
            }
            catch (System.Exception ex)
            {
               
            }

        }
        /// <summary>
        /// 외부에서 호출: 차트 데이터 갱신(모든 차트 타입 공통)  
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public async Task RefreshAsync(ChartData newData)
        {
            //1️. chart 데이터 갱신
            chartData = newData;
            //2.재마운트로 완전 초기화
            /* 즉, @key가 바뀌면: 
             * 이전 chartRef와 연결된 DOM 요소 삭제
             * 새<canvas> 생성
             * 새 C# 객체와 새 JS Chart 인스턴스가 다시 생성됨
             이 덕분에 완전히 “초기상태”로 다시 렌더링.*/
            _renderKey = Guid.NewGuid();
            chartRef = null;
            isReady = false; 
            StateHasChanged();

            //DOM 렌더링 기다림(Canvas 생성 보장)
            await Task.Delay(250);
            
            isReady = true;
            StateHasChanged();

            // 3. ref 확보 → 데이터 세팅
            await WaitForChartReference();
            await InitializeChartAsync();

            await TriggerChartResizeAsync();

            Console.WriteLine($"🔁 [{ChartType}] refreshed.");
        }



    }   
}
