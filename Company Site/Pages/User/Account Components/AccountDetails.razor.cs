using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Account_Components
{
    public partial class AccountDetails : ComponentBase
    {

        #region Public Members

        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        private AccountDetails Details { get; set; } = new AccountDetails();

        private List<string> _errors = new List<string>();

        #endregion

        #region Injected Members

        [Inject]
        private ProtectedBrowserStorage _storage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Once the control has been initialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (_dbContext.AccountDetails.Any(a => a.UserId == UserId.Value))
            {
                Details = _dbContext.AccountDetails.Where(a => a.UserId == UserId.Value).First();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves changes to the account details
        /// </summary>
        private void Save()
        {
            if(_dbContext.AccountDetails.Any(a => a.UserId == UserId.Value )) 
            {
                _dbContext.Update(Details);
                _dbContext.SaveChanges();
                //TODO: Navigate to the account management page
            }
            else
            {
                Details.UserId = UserId.Value;
                _dbContext.AccountDetails.Add(Details);
                _dbContext.SaveChanges();
                //TODO: Navigate back to the accounts management page
            }
        }

        #endregion
    }
}
