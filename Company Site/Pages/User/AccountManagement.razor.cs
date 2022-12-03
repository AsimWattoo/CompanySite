using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Radzen;

namespace Company_Site.Pages.User
{
    public partial class AccountManagement : BaseAddPage<TrustRelationModel, int>
    {

        #region Public Properties

        /// <summary>
        /// The user id of the logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The selected account
        /// </summary>
        private Account? _Account { get; set; }

        /// <summary>
        /// The list of all the accounts
        /// </summary>
        private List<Account> Accounts { get; set; } = new List<Account>();

        /// <summary>
        /// The list of filtered accounts
        /// </summary>
        private List<Account> FilteredAccounts { get; set; } = new List<Account>();

        private Dictionary<string, Func<TrustRelationModel, string>> Headers { get; set; } = new Dictionary<string, Func<TrustRelationModel, string>>()
        {
            ["Trust"] = t => t.Trust.Trust_Name,
            ["Total SR Issued"] = t => t.Trust.SRIssued.ToString(),
        };

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when it is initialized
        /// </summary>
        protected override void Setup()
        {
            _dbSet = _dbContext.TrustRelations;
            Accounts = _dbContext.Accounts.ToList();
            FilteredAccounts = Accounts;
        }

        #endregion

        #region Private Methods


        private void OnAccountChange(object account)
        {
            if(account is int borrowerCode) 
            {
                _applicationState.BorrowerCode = borrowerCode;
                LoadData();
                _Account = Accounts.Where(f => f.BorrowerCode == borrowerCode).First();
                SaveResetup();
            }
        }

        void LoadAccountData(LoadDataArgs args)
        {
            var query = Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(args.Filter))
            {
                query = query.Where(c => c.Company.ToLower().Contains(args.Filter.ToLower()));
            }

            FilteredAccounts = query.ToList();

            InvokeAsync(StateHasChanged);
        }

        private int GetId(TrustRelationModel model) => model.Id;

        protected override void LoadData()
        {
            Enteries = _dbContext.TrustRelations.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            Enteries.ForEach(e =>
            {
                e.Account = _dbContext.Accounts.Where(a => a.BorrowerCode == e.BorrowerCode).FirstOrDefault();
                e.Trust = _dbContext.Trusts.Where(t => t.TrustCode == e.TrustCode).FirstOrDefault();
            });
        }

        #endregion

    }
}
