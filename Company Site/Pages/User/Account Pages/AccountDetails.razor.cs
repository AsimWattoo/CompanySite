using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

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

        private AccountDetailsModel Details { get; set; } = new AccountDetailsModel();

        private List<string> _errors = new List<string>();

        private List<string> bankSuggestions = new List<string>();

        private List<Trust> Trusts = new List<Trust>();

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ApplicationState State { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Once the control has been initialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Trusts = _dbContext.Trusts.ToList();
            bankSuggestions = _dbContext.AccountDetails.Select(a => a.Bank).Distinct().ToList();
            CreateNewInstance();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if (_dbContext.AccountDetails.Any(a => a.BorrowerCode == State.BorrowerCode)) 
            {
                _dbContext.Update(Details);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.AccountDetails.Add(Details);
                _dbContext.SaveChanges();
            }
        }

        private void Clear()
        {
            CreateNewInstance();
        }

        private void CreateNewInstance()
        {
            Details = new AccountDetailsModel();
            Data.User user = _dbContext.Users.Where(u => u.Id == UserId.Value).First();
            if (State.BorrowerCode != -1)
            {
                if (_dbContext.AccountDetails.Any(a => a.BorrowerCode == State.BorrowerCode))
                {
                    Details = _dbContext.AccountDetails.Where(a => a.BorrowerCode == State.BorrowerCode).First();
                    Details.Modifier = $"{user.FirstName} {user.LastName}";
                    Details.ModificationDate = DateTime.Now;
                }
                else
                {
                    Details.BorrowerCode = State.BorrowerCode;
                    Account? acc = _dbContext.Accounts.Where(a => a.BorrowerCode == State.BorrowerCode).FirstOrDefault();
                    Details.TrustCode = acc.TrustCode;
                    Details.CreatorName = $"{user.FirstName} {user.LastName}";
                    Details.CreationDate = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
