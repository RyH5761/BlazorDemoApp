namespace BlazorDemoApp.Admin.Components.Pages.UI
{
    public partial class CardsPage : ComponentBase
    {
        private string? SelectedPeriod = "주간";

        private Dictionary<string, string> PeriodOptions = new()
        {
            ["daily"] = "일간",
            ["weekly"] = "주간",
            ["monthly"] = "월간"
        };

        private List<(string Text, string Icon)> TodoList = new()
        {
            ("할 일 1 완료", "fa fa-check text-success"),
            ("진행 중", "fa fa-sync text-warning"),
            ("미완료", "fa fa-times text-danger")
        };
    }
}
