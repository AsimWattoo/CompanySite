using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class DateFilter : ComponentBase
    {
        #region Parameters

        [Parameter]
        public Action<DateTime, DateTime>? SearchRecords { get; set;}

        [Parameter]
        public Action? Clear { get; set; }

        #endregion

        #region Private Properties

        private DateTime StartDate { get; set; } = DateTime.Now;

        private DateTime EndDate { get; set; } = DateTime.Now;

        #endregion
    }
}
