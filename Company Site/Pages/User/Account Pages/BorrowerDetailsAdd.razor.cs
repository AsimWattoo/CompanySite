using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class BorrowerDetailsAdd : ComponentBase
    {

        #region Private Members

        private bool IsAddMode { get; set; } = true;

        private BorrowerDetail NewEntry { get; set; } = new BorrowerDetail();

        private List<string> _errors = new List<string>();

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ProtectedSessionStorage _storage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        #endregion

        #region Overriden Methods

        protected override async void OnInitialized()
        {
            ProtectedBrowserStorageResult<string> pageModeResult = await _storage.GetAsync<string>("BorrowerPageMode");
            if (pageModeResult.Success)
            {
                if(pageModeResult.Value == "edit")
                {
                    IsAddMode = false;
                    ProtectedBrowserStorageResult<int> idRes = await _storage.GetAsync<int>("BorrowerId");
                    if(idRes.Success)
                    {
                        NewEntry = _dbContext.BorrowerDetails.Where(a => a.Id == idRes.Value).First();
                        StateHasChanged();
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void Add()
        {
            if (IsAddMode)
                _dbContext.BorrowerDetails.Add(NewEntry);
            else
                _dbContext.Update(NewEntry);
            _dbContext.SaveChanges();
            _navigationManager.NavigateTo("/borrowerdetails");
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/borrowerdetails");
        }

        #endregion
    }
}
