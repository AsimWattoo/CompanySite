using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class MultiErrorView : ComponentBase
    {
        #region Public Properties

        [Parameter]
        public List<string> Errors { get; set; } = new List<string>();

        #endregion
    }
}
