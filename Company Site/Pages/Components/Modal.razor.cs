using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class Modal: ComponentBase
    {

        #region Parameters

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool ShowModal { get; set; } = false;

        [Parameter]
        public string Title { get; set; }

        #endregion

    }
}
