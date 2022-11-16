using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Radzen;

namespace Company_Site.Pages.User
{
    public partial class AccountManagement : BaseAddPage<TrustRelationModel>
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

        /// <summary>
        /// The list of available trusts
        /// </summary>
        private List<Trust> Trusts { get; set; } = new List<Trust>();

        /// <summary>
        /// Indicates that the modal should be shown
        /// </summary>
        private bool ShowModel { get; set; } = false;

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
            Trusts = _dbContext.Trusts.ToList();
        }

        #endregion

        #region Private Methods


        private void OnAccountChange(object account)
        {
            if(account is int borrowerCode) 
            {
                _applicationState.BorrowerCode = borrowerCode;
                Enteries = _dbContext.TrustRelations.Where(t => t.BorrowerCode == borrowerCode).ToList();
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

        /// <summary>
        /// Runs at the end of the save method
        /// Used to resetup things for next input
        /// </summary>
        protected override void SaveResetup()
        {
            NewEntry = new TrustRelationModel();
            if(_applicationState.BorrowerCode != -1)
            {
                NewEntry.Account = _dbContext.Accounts.Where(a => a.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
            }
            NewEntry.Trust = new Trust();
        }
        
        /// <summary>
        /// Runs at start of the save method
        /// If it is true then rest of the save code is executed
        /// </summary>
        /// <returns></returns>
        protected override bool SaveSetup()
        {
            if(NewEntry.Account == null)
            {
                _errors.Add("No Account exists");
                return false;
            }
            else if(_dbContext.TrustRelations.Count(t => t.BorrowerCode == _applicationState.BorrowerCode && t.TrustCode == NewEntry.TrustCode) > 0)
            {
                _errors.Add("Trust Code is already added for the current account.");
                return false;
            }
            else
            {
                ShowModel = false;
                return base.SaveSetup();
            }
        }

        private int GetId(TrustRelationModel model) => model.Id;

        private void TrustCodeChanged(string trustCode)
        {
            NewEntry.TrustCode = trustCode;
            NewEntry.Trust = _dbContext.Trusts.Where(t => t.TrustCode == trustCode).FirstOrDefault();
        }

        public override void OnEdit(int id)
        {
            ShowModel = true;
        }

        protected override void OnClear()
        {
        }

        protected override void LoadData()
        {
            Enteries = _dbContext.TrustRelations.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            Enteries.ForEach(e =>
            {
                e.Account = _dbContext.Accounts.Where(a => a.BorrowerCode == e.BorrowerCode).FirstOrDefault();
                e.Trust = _dbContext.Trusts.Where(t => t.TrustCode == e.TrustCode).FirstOrDefault();
            });
        }

        private void Cancel()
        {
            ShouldAdd = true;
            ShowModel = false;
            SaveResetup();
        }

        #endregion

    }
}
