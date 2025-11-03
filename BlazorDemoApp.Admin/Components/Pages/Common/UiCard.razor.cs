namespace BlazorDemoApp.Admin.Components.Pages.Common
{
    public  partial class UiCard : ComponentBase
    {
        [Parameter] public string HeaderTitle { get; set; } = "Card Title";
        [Parameter] public string HeaderColor { get; set; } = "#4e73df";
        [Parameter] public string MaxWidth { get; set; } = "100%";
        [Parameter] public string MaxHeight { get; set; } = "Auto";
        [Parameter] public CardTypes Type { get; set; } = CardTypes.Basic;
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private string CardCss =>
                      $"card shadow-sm m-2 {(Type == CardTypes.DropDown ? "has-dropdown" :
                             Type == CardTypes.Collapse ? "has-collapse" :
                             Type == CardTypes.Filter ? $"has-filter{(IsFilterOpen ? " filter-open" : "")}" :
                             "")}";


        /// <summary>
        /// 공통요소 선언
        /// </summary>
        [Parameter] public string? Title { get; set; }
        [Parameter] public string? Content { get; set; }
        [Parameter] public bool IsLoading { get; set; }
        /// <summary>
        /// 이미지 관련 요소 정의
        /// </summary>
        [Parameter] public string? ImageSrc { get; set; }
        [Parameter] public string? Description { get; set; }
        
        /// <summary>
        /// 상태확인 요소 정의
        /// </summary>
        [Parameter] public string? Value { get; set; }
        [Parameter] public string? SubText { get; set; }
        /// <summary>
        /// 리스트 아이템 요소 정의
        /// </summary>
        [Parameter] public List<(string Text, string Icon)>? Items { get; set; }
        /// <summary>
        /// 차트 요소 정의
        /// </summary>
        //[Parameter] public ChartData<double>? ChartData { get; set; }
        //[Parameter] public LineChartOptions? ChartOptions { get; set; }
        /// <summary>
        /// 아이콘 요소 정의
        /// </summary>
        [Parameter] public string? Icon { get; set; }
        /// <summary>
        /// dropdown 요소 정의
        /// </summary>
        [Parameter] public Dictionary<string, string>? DropDownItems { get; set; }
        [Parameter] public string? SelectedItem { get; set; }
        [Parameter] public EventCallback<string> SelectedItemChanged { get; set; }
       
        private bool showMenu;
        private bool IsCollapsed = false;
        private bool IsFilterOpen = false;

        [Parameter] public bool DefaultCollapsed { get; set; } = false;

        /// <summary>
        /// 토글 메뉴 표시/숨김
        /// </summary>
        private void ToggleMenu() => showMenu = !showMenu;
        private void ToggleCollapse() => IsCollapsed = !IsCollapsed;
        private void ToggleFilter() => IsFilterOpen = !IsFilterOpen;

        protected override void OnInitialized()
        {
            IsCollapsed = DefaultCollapsed;
            base.OnInitialized();
        }



        /// <summary>
        /// 드롭다운 메뉴 선택처리
        /// </summary>
        /// <param name="key"></param>
        /// <param name="display"></param>
        /// <returns></returns>
        private async Task SelectMenu(string key, string value)
        { 
            SelectedItem = value;   // 카드 내부 값 변경
            
            showMenu = false;         // 메뉴 닫기

            if (SelectedItemChanged.HasDelegate)
                await SelectedItemChanged.InvokeAsync(value);

            StateHasChanged();
        }

        /// <summary>
        /// 기본 카드형
        /// </summary>
        /// <returns></returns>
        private RenderFragment BasicCard() => builder =>
        {
            builder.OpenElement(0, "p");
            builder.AddAttribute(1, "class", "mb-0");
            builder.AddAttribute(2, "style", $"height:{MaxHeight}; overflow:hidden; position:relative;");
            builder.AddContent(3, Content ?? "기본 카드입니다.");
            builder.CloseElement();
        };
        /// <summary>
        /// 이미지 카드형
        /// </summary>
        /// <returns></returns>
        private RenderFragment ImageCard() => builder =>
        {
            builder.OpenElement(0, "div");
            // 부모를 위치 기준으로 설정
            builder.AddAttribute(1, "class", "position-relative w-100");
            builder.AddAttribute(2, "style", $"height:{MaxHeight}; overflow:hidden;");

            // 이미지가 기본설정
            if (!string.IsNullOrEmpty(ImageSrc))
            {
                builder.OpenElement(3, "img");
                builder.AddAttribute(4, "src", ImageSrc);
                builder.AddAttribute(5, "alt", Title ?? "Image");
                builder.AddAttribute(6, "class", "rounded shadow-sm");
                // absolute 중앙정렬 + scale + 비율유지
                builder.AddAttribute(7, "style",
                    "position:absolute;" +
                    "top:50%; left:50%;" +
                    "transform:translate(-50%, -50%) scale(0.85);" +
                    "object-fit:contain;" +
                    "max-width:80%; max-height:80%;" +
                    "transition:transform 0.3s ease;" +
                    "display:block;");

                /*사진 썸네일 스타일: 카드를 꽉채우는 스타일*/
                //builder.AddAttribute(10, "style", "height:100%; width:100%; object-fit:cover;");

                builder.CloseElement(); // img    
            }
            // 제목
            if (!string.IsNullOrEmpty(Title))
            {
                builder.OpenElement(8, "h6");
                builder.AddAttribute(9, "class", "fw-bold text-center mt-2 mb-1");
                builder.AddContent(10, Title);
                builder.CloseElement();
            }

            // 설명
            if (!string.IsNullOrEmpty(Description))
            {
                builder.OpenElement(11, "p");
                builder.AddAttribute(12, "class", "text-muted text-center small mb-0");
                builder.AddContent(13, Description);
                builder.CloseElement();
            }

            builder.CloseElement(); // div
        };
        /// <summary>
        ///  상태확인 카드형
        /// </summary>
        /// <returns></returns>
        private RenderFragment StatsCard() => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "d-flex flex-column justify-content-center align-items-center");
            builder.AddAttribute(2, "style", $"height:100%;");

            builder.OpenElement(3, "h5");
            builder.AddAttribute(4, "class", "fw-bold text-uppercase mb-2");
            builder.AddContent(5, Title ?? "통계");
            builder.CloseElement();

            builder.OpenElement(6, "div");
            builder.AddAttribute(7, "class", "display-6 fw-bold text-primary");
            builder.AddContent(8, Value ?? "0");
            builder.CloseElement();

            if (!string.IsNullOrEmpty(SubText))
            {
                builder.OpenElement(9, "p");
                builder.AddAttribute(10, "class", "text-muted small mb-0");
                builder.AddContent(11, SubText);
                builder.CloseElement();
            }

            builder.CloseElement();
        };
        /// <summary>
        /// 리스트 카드형 
        /// </summary>
        /// <returns></returns>
        private RenderFragment ListCard() => builder =>
        {
            /*<ul class="list-group text-start">
                <li class="list-group-item d-flex align-items-center border-0 p-1">
                    <i class="fa fa-check text-success me-2"></i>
                        <span>할 일 1 완료</span>
                    </li>
                <li class="list-group-item d-flex align-items-center border-0 p-1">
                    <i class="fa fa-sync text-warning me-2"></i>
                        <span>진행 중</span>
                    </li>
                <li class="list-group-item d-flex align-items-center border-0 p-1">
                    <i class="fa fa-times text-danger me-2"></i>
                        <span>미완료</span>
                    </li>
            </ul>*/

                if (Items is null || Items.Count == 0)
            {
                builder.OpenElement(0, "p");
                builder.AddAttribute(1, "class", "text-muted");
                builder.AddContent(2, "표시할 항목이 없습니다.");
                builder.CloseElement();
                return;
            }

            builder.OpenElement(3, "ul");
            builder.AddAttribute(4, "class", "list-group text-start");

            foreach (var item in Items)
            {
                builder.OpenElement(5, "li");
                builder.AddAttribute(6, "class", "list-group-item d-flex align-items-center border-0 p-1");

                if (!string.IsNullOrWhiteSpace(item.Icon))
                {
                    builder.OpenElement(7, "i");
                    builder.AddAttribute(8, "class", $"{item.Icon} me-2");
                    builder.CloseElement();
                }

                builder.OpenElement(9, "span");
                builder.AddContent(10, item.Text);
                builder.CloseElement();

                builder.CloseElement(); // li
            }

            builder.CloseElement(); // ul
        };
        /// <summary>
        /// 로딩 카드형
        /// </summary>
        /// <returns></returns>
        private RenderFragment LoadingCard() => __builder =>
        {
            if (IsLoading)
            {
                __builder.AddMarkupContent(0, $@"
                <div class='d-flex justify-content-center align-items-center' style='height:{MaxHeight};'>
                    <div class='spinner-border text-primary' role='status'>
                        <span class='visually-hidden'>Loading...</span>
                    </div>
                </div>");
            }
            else if (ChildContent is not null)
            {
                __builder.AddContent(1, ChildContent);
            }
        };
        /// <summary>
        /// 아이콘 카드형
        /// </summary>
        /// <returns></returns>
        private RenderFragment IconCard() => builder =>
        {
            builder.OpenElement(0, "div");
            // flex 기반 정렬
            builder.AddAttribute(1, "class", "d-flex justify-content-between align-items-center w-100");
            builder.AddAttribute(2, "style", $"min-height:70px; padding:0.5rem 0;");

            // 왼쪽 텍스트 영역
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "text-start ms-2");

            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", "text-uppercase text-muted small mb-1");
            builder.AddContent(7, Title ?? "Title");
            builder.CloseElement(); // small text

            builder.OpenElement(8, "div");
            builder.AddAttribute(9, "class", "h5 fw-bold mb-0 text-dark");
            builder.AddContent(10, Value ?? "-");
            builder.CloseElement(); // value

            builder.CloseElement(); // left div

            // 오른쪽 아이콘 영역
            if (!string.IsNullOrEmpty(Icon))
            {
                builder.OpenElement(11, "div");
                builder.AddAttribute(12, "class", "me-3 d-flex align-items-center justify-content-center");
                builder.OpenElement(13, "i");
                builder.AddAttribute(14, "class", $"{Icon} fa-2x text-secondary");
                builder.CloseElement(); // i
                builder.CloseElement(); // right div
            }

            builder.CloseElement(); // outer flex
        };
        /// <summary>
        /// 드랍다운 카드형
        /// </summary>
        /// <returns></returns>
        protected RenderFragment DropDownCard() => builder =>
        {
            // -------------------------------
            // 📄 원래 마크업 형태 (참고용)
            // -------------------------------
            /*
            <div class="py-3 text-center">
                <div class="lead mb-2">현재 선택: @SelectedItem</div>
            </div>
            */
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "py-3 text-center");

            var text = SelectedItem ?? "선택된 값이 없습니다.";
            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "lead mb-0");
            builder.AddContent(4, $"현재 선택: {text}");
            builder.CloseElement();

            builder.CloseElement();
        };

        /// <summary>
        /// 접힘/보임 카드형
        /// </summary>
        /// <returns></returns>
        protected RenderFragment CollapseCard() => builder =>
        {
            var expandedClass = IsCollapsed ? "" : "expanded";

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", $"card-collapse-panel {expandedClass} ");
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        };

        // Filter 카드
        protected RenderFragment FilterCard() => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", $"card has-filter {(IsFilterOpen ? "filter-open" : "")}");
            builder.AddContent(2, ChildContent);

            // 필터 메뉴 (슬라이드)
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "filter-menu");
            builder.AddContent(5, (RenderFragment)(menu =>
            {
                menu.OpenElement(0, "h6");
                menu.AddContent(1, "Filter 옵션");
                menu.CloseElement();
                menu.OpenElement(2, "p");
                menu.AddContent(3, "필터 내용이 여기에 표시됩니다.");
                menu.CloseElement();
            }));
            builder.CloseElement();

            builder.CloseElement();
        };

        // DropDown 버튼 전용 RenderFragment
        protected RenderFragment DropDownButton() => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "dropdown");
            builder.AddAttribute(2, "tabindex", "0");
            builder.AddAttribute(3, "style", "position:absolute; right:10px; top:50%; transform:translateY(-50%);");
            builder.AddAttribute(4, "key", showMenu);

            builder.OpenElement(5, "button");
            builder.AddAttribute(6, "type", "button");
            builder.AddAttribute(7, "class", "btn btn-sm btn-outline-light text-dark");
            builder.AddAttribute(8, "style", "display:flex; align-items:center; justify-content:center; width:32px; height:32px; border-radius:0.25rem;");
            builder.AddAttribute(9, "onclick", EventCallback.Factory.Create(this, ToggleMenu));
            builder.OpenElement(10, "i");
            builder.AddAttribute(11, "class", "fas fa-ellipsis-v");
            builder.CloseElement();
            builder.CloseElement();

            builder.OpenElement(12, "ul");
            builder.AddAttribute(13, "class", "list-unstyled m-0 p-0 position-absolute");
            builder.AddAttribute(14, "hidden", showMenu ? null : true);
            builder.AddAttribute(15, "style", "right:0; top:100%; margin-top:6px; min-width:140px; z-index:1000; background:#fff; border:1px solid #dee2e6; border-radius:.375rem; box-shadow:0 4px 12px rgba(0,0,0,.1);");

            if (DropDownItems != null)
            {
                foreach (var kvp in DropDownItems)
                {
                    var key = kvp.Key;
                    var value = kvp.Value;

                    builder.OpenElement(16, "li");
                    builder.OpenElement(17, "button");
                    builder.AddAttribute(18, "type", "button");
                    builder.AddAttribute(19, "class", "dropdown-item w-100 text-start");
                    builder.AddAttribute(20, "style", "padding:.4rem .75rem; background:transparent; border:0;");
                    builder.AddAttribute(21, "onclick", EventCallback.Factory.Create(this, () => SelectMenu(key, value)));
                    builder.AddContent(22, value);
                    builder.CloseElement(); // button
                    builder.CloseElement(); // li
                }
            }

            builder.CloseElement(); // ul
            builder.CloseElement(); // div
        };



    }
}
