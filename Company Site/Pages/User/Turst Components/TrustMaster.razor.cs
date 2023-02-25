using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Helpers;
using Company_Site.Interfaces;
using Company_Site.ViewModels;

namespace Company_Site.Pages.User.Turst_Components
{

    public partial class TrustMaster : BaseModifierAddPage<Trust, int>, ITable<Trust, int>
    {
        #region Private Members

        public Dictionary<string, Func<Trust, string>> Headers { get; set; } = new Dictionary<string, Func<Trust, string>>()
        {
            ["Trust Date"] = (Trust e) => e.TrustSetupDate.ToString("dd-MMM-yyyy"),
            ["SR Holder"] = (Trust e) => e.SRHolder,
            ["Ratio"] = (Trust e) => e.Ratio.ToString(),
            ["Upside"] = (Trust e) => e.IssuerUpsideShare.ToString(),
            ["SR Issued"] = (Trust e) => e.SRIssued.ToString(),
            ["SR O/s"] = (Trust e) => e.SROs,
            ["Turst Age"] = (Trust e) => e.TrustAge.ToString(),
            ["Trusteeship Fee"] = (Trust e) => Number.Round(GetTrusteeshipFee(e)),
            ["Resolution Fee"] = (Trust e) => e.ResolutionFee.ToString(),
        };

        #endregion

        #region Account Table Fields

        private List<AccountViewModel> TrustAccounts { get; set; } = new List<AccountViewModel>();

        private Dictionary<string, Func<AccountViewModel, string>> AccountHeaders { get; set; } = new Dictionary<string, Func<AccountViewModel, string>>()
        {
            ["Borrower Code"] = e => e.BorrowerCode.ToString(),
            ["Company Name"] = e => e.Company,
            ["Acquisition Date"] = e => e.AcquistionDate.ToString("dd-MMM-yyyy"),
            ["Acquisition Price"] = e => e.AcquisitonPrice.ToString(),
            ["SR Issued"] = e => e.SRIssued.ToString(),
        };

        private int GetAccountId(AccountViewModel acc) => acc.BorrowerCode;

        /// <summary>
        /// The new account which is being edited
        /// </summary>
        private AccountViewModel NewAccount { get; set; } = new AccountViewModel();

        private bool ShowAccountForm { get; set; } = false;

        private bool AccountAddMode { get; set; } = true;

        #endregion

        #region Link Account Fields

        public List<Account> ExistingAccounts { get; set; } = new List<Account>();

        public Account SelectedExistingAccount { get; set; } = new Account();

        public void OnExistingAccountChange(object account)
        {
            if(account is int borrowerCode)
            {
                SelectedExistingAccount = ExistingAccounts.Where(f => f.BorrowerCode == borrowerCode).First();
            }
        }

        public bool ShowExistingAccountForm { get; set; } = false;

        public void ShowExistingAccounts()
        {
            SelectedExistingAccount = new Account();
            List<int> linkedAccounts = _dbContext.TrustRelations.Where(e => e.TrustCode == NewEntry.TrustCode).Select(e => e.BorrowerCode).ToList();
            ExistingAccounts = _dbContext.Accounts.Where(e => !linkedAccounts.Contains(e.BorrowerCode)).ToList();
            ShowExistingAccountForm = true;
        }

        public void CancelExistingAccountForm()
        {
            ShowExistingAccountForm = false;
        }

        public void LinkExistingAccount()
        {
            TrustRelationModel model = new TrustRelationModel() { BorrowerCode = SelectedExistingAccount.BorrowerCode, TrustCode = NewEntry.TrustCode, AcquisitonPrice = NewAccount.AcquisitonPrice, AcquistionDate = NewAccount.AcquistionDate, Assignor = NewAccount.Assignor, SRIssued = NewAccount.SRIssued };
            _dbContext.TrustRelations.Add(model);
            _dbContext.SaveChanges();
            TrustAccounts.Add(new AccountViewModel(SelectedExistingAccount, NewEntry, model));
            NewAccount = new AccountViewModel();
            CancelExistingAccountForm();
            StateHasChanged();
        }

        #endregion

        #region Account Methods

        private void OnAccountEdit(int borrowerCode)
        {
            AccountAddMode = false;
            ShowAccountForm = true;
            NewAccount = TrustAccounts.Where(a => a.BorrowerCode == borrowerCode).FirstOrDefault() ?? new AccountViewModel();
            StateHasChanged();
        }

        private List<AccountViewModel> OnAccountDelete(int borrowerCode)
        {
            AccountViewModel model = TrustAccounts.Where(e => e.BorrowerCode == borrowerCode).First();
            TrustRelationModel? relation = _dbContext.TrustRelations.Where(a => a.BorrowerCode == borrowerCode && a.TrustCode == model.TrustCode).FirstOrDefault();
            if (relation == null)
                return TrustAccounts;

            _dbContext.TrustRelations.Remove(relation);
            _dbContext.SaveChanges();
            StateHasChanged();
            TrustAccounts.Remove(model);
            return TrustAccounts;
        }

        private void AddAccount()
        {
            CreateNewAccount();
            ShowAccountForm = true;
        }

        private void CancelAccountForm()
        {
            ShowAccountForm = false;
            AccountAddMode = true;
        }

        private void ClearAccountForm()
        {
            CreateNewAccount();
        }

        private void SaveAccount()
        {
            if (AccountAddMode)
            {
                Account account = new Account()
                {
                    BorrowerCode = NewAccount.BorrowerCode,
                    Company = NewAccount.Company,
                };
                TrustRelationModel relation = new TrustRelationModel()
                {
                    BorrowerCode = account.BorrowerCode,
                    AcquisitonPrice = NewAccount.AcquisitonPrice,
                    AcquistionDate = NewAccount.AcquistionDate,
                    Assignor = NewAccount.Assignor,
                    TrustCode = NewAccount.TrustCode,
                    SRIssued = NewAccount.SRIssued,
                };
                _dbContext.Accounts.Add(account);
                _dbContext.TrustRelations.Add(relation);
            }
            else
            {
                Account acc = _dbContext.Accounts.Where(f => f.BorrowerCode == NewAccount.BorrowerCode).First();
                acc.Company = NewAccount.Company;
                _dbContext.Accounts.Update(acc);
                TrustRelationModel trustRelation = _dbContext.TrustRelations.Where(f => f.BorrowerCode == NewAccount.BorrowerCode && f.TrustCode == NewAccount.TrustCode).First();
                trustRelation.Assignor = NewAccount.Assignor;
                trustRelation.AcquisitonPrice = NewAccount.AcquisitonPrice;
                trustRelation.AcquistionDate = NewAccount.AcquistionDate;
                trustRelation.SRIssued = NewAccount.SRIssued;
                _dbContext.TrustRelations.Update(trustRelation);
            }
            _dbContext.SaveChanges();
            LoadTrustAccounts();
            CancelAccountForm();
            StateHasChanged();
        }

        private void CreateNewAccount()
        {
            NewAccount = new AccountViewModel();
            Account? acc = _dbContext.Accounts.OrderByDescending(f => f.BorrowerCode).FirstOrDefault();
            NewAccount.BorrowerCode = acc == null ? 1 : acc.Id + 1;
            NewAccount.Assignor = NewEntry.SRHolder;
            NewAccount.TrustCode = NewEntry.TrustCode;
            NewAccount.TrustName = NewEntry.Trust_Name;
        }

        /// <summary>
        /// Loads accounts based on trust relation models
        /// </summary>
        private void LoadTrustAccounts()
        {
            List<TrustRelationModel> relations = _dbContext.TrustRelations.Where(f => f.TrustCode == NewEntry.TrustCode).ToList();
            List<Account> accounts = _dbContext.Accounts.ToList();
            TrustAccounts = new List<AccountViewModel>();
            relations.ForEach(e =>
            {
                Account acc = accounts.Where(f => f.BorrowerCode == e.BorrowerCode).First();
                TrustRelationModel rel = _dbContext.TrustRelations.Where(f => f.BorrowerCode == e.BorrowerCode && f.TrustCode == NewEntry.TrustCode).First();
                AccountViewModel accountViewModel = new AccountViewModel(acc, NewEntry, rel);
                TrustAccounts.Add(accountViewModel);
            });
        }

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.Trusts;
        }

        protected override void OnClear()
        {
            base.OnClear();
            TrustAccounts = new List<AccountViewModel>();
        }

        #endregion

        #region Private Members

        private static double GetTrusteeshipFee(Trust t)
        {
            if (t.TrustAge <= 1)
                return t.T_Year1;
            else if (t.TrustAge > 1 && t.TrustAge <= 2)
                return t.T_Year2;
            else if (t.TrustAge > 2 && t.TrustAge <= 3)
                return t.T_Year3;
            else if (t.TrustAge > 3 && t.TrustAge <= 4)
                return t.T_Year4;
            else if (t.TrustAge > 4 && t.TrustAge <= 5)
                return t.T_Year5;
            else if (t.TrustAge > 5 && t.TrustAge <= 6)
                return t.T_Year6;
            else if (t.TrustAge > 6 && t.TrustAge <= 7)
                return t.T_Year7;
            else if (t.TrustAge > 7 && t.TrustAge <= 8)
                return t.T_Year8;
            else
                return t.T_Year9;
        }

        #endregion

        #region Public Methods

        public void ClearTrusteeshipFee()
        {
            NewEntry.T_Year1 = NewEntry.T_Year2 = NewEntry.T_Year3 = NewEntry.T_Year4 = NewEntry.T_Year5 = NewEntry.T_Year6 = NewEntry.T_Year7 = NewEntry.T_Year8 = NewEntry.T_Year9 = 0;
        }

        public void ClearResolutionFee()
        {
            NewEntry.R_Year1 = NewEntry.R_Year2 = NewEntry.R_Year3 = NewEntry.R_Year4 = NewEntry.R_Year5 = NewEntry.R_Year6 = NewEntry.R_Year7 = NewEntry.R_Year8 = NewEntry.R_Year9 = 0;
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(Trust e) => e.Id;

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<Trust> Search(List<Trust> enteries, string text)
        {
            text = text.ToLower();
            return enteries.Where(e => e.TrustCode.Contains(text) || e.SRHolder.Contains(text) || e.IssuerShare.ToString().Contains(text) || e.HolderShare.ToString().Contains(text)).ToList();
        }

        public override void OnEdit(int id)
        {
            LoadTrustAccounts();
            RecordData();
        }

        #endregion
    }
}
