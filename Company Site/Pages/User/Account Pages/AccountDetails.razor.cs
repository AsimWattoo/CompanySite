using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class AccountDetails : ComponentBase
    {

        #region Public Members

        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        private Account Details { get; set; } = new Account();

        private List<string> _errors = new List<string>();

        private List<string> bankSuggestions = new List<string>();

        #endregion

        #region Injected Members

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
            Data.User user = _dbContext.Users.Where(u => u.Id == UserId.Value).First();
            bankSuggestions = _dbContext.Accounts.Select(a => a.Bank).Distinct().ToList();
            if (_dbContext.Accounts.Any(a => a.UserId == UserId.Value))
            {
                Details = _dbContext.Accounts.Where(a => a.UserId == UserId.Value).First();
                Details.Modification = $"{user.FirstName} {user.LastName}";
                Details.ModificationDate = DateTime.Now;
            }
            else
            {
                Details.UserId = UserId.Value;
                Details.Creator_Name = $"{user.FirstName} {user.LastName}";
                Details.CreationDate = DateTime.Now;

                //If db context contains any account recortd
                if (_dbContext.Accounts.Any())
                {
                    //TODO: Generate Borrower Code
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves changes to the account details
        /// </summary>
        private void Save()
        {
            if (_dbContext.Accounts.Any(a => a.UserId == UserId.Value )) 
            {
                _dbContext.Update(Details);
                _dbContext.SaveChanges();
                //TODO: Navigate to the account management page
            }
            else
            {
                _dbContext.Accounts.Add(Details);
                _dbContext.SaveChanges();
                //TODO: Navigate back to the accounts management page
            }
        }

        /// <summary>
        /// Cancels the editing
        /// </summary>
        private void Cancel()
        {
            //TODO: Navigate to the Accounts Management Page
        }

        #endregion
    }
}
