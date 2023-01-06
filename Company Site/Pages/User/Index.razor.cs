using Company_Site.Pages.Components;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class Index : ComponentBase
    {
        #region Private Members

        private Calendar calender = new Calendar();

        #endregion

        #region Private Methods

        /// <summary>
        /// Refreshes the state of the component
        /// </summary>
        private void RefreshState()
        {
            StateHasChanged();
            calender.UpdateState();
        }

        #endregion
    }
}
