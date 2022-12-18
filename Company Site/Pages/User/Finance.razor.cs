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
                else if (res.Value == "collection")
                    CurrentType = FinanceTypes.Collection;
                else if (res.Value == "distribution")
                    CurrentType = FinanceTypes.Distribution;
                await _sessionStorage.DeleteAsync("FinancePageMode");
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
            CurrentType = fType;
        }

        #endregion
    }
}
