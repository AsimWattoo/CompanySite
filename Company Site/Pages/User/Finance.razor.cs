using Company_Site.Enum;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User
{
    public partial class Finance : ComponentBase
    {
        #region Private Members

        private FinanceTypes CurrentType { get; set; } = FinanceTypes.None;

        #endregion

        #region Injected Members

        [Inject]
        private ProtectedSessionStorage _sessionStorage { get; set; }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Fires after the component is initialized
        /// </summary>
        protected override async void OnInitialized()
        {
            base.OnInitialized();

            ProtectedBrowserStorageResult<string> res = await _sessionStorage.GetAsync<string>("FinancePageMode");
            if (res.Success)
            {
                if (res.Value == "expense")
                    CurrentType = FinanceTypes.Expenses;

            }
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the display of the finance page
        /// </summary>
        /// <param name="fType"></param>
        private void _changePage(FinanceTypes fType)
        {
            if (fType == CurrentType)
                CurrentType = FinanceTypes.None;
            else
                CurrentType = fType;
        }

        #endregion
    }
}
