using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class CollapsibleContainer : ComponentBase
    {

        #region Parameters

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Id { get; set; } = 0;

        #endregion

    }
}
