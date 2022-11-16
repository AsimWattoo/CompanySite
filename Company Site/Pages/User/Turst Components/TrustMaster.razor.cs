using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Helpers;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Turst_Components
{

    public partial class TrustMaster : BaseModifierAddPage<Trust>, ITable<Trust, int>
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

        private List<Account> TrustAccounts { get; set; } = new List<Account>();

        private Dictionary<string, Func<Account, string>> AccountHeaders { get; set; } = new Dictionary<string, Func<Account, string>>()
        {
            ["Borrower Code"] = e => e.BorrowerCode.ToString(),
            ["Company Name"] = e => e.Company,
            ["Acquisition Date"] = e => e.AcquistionDate.ToString("dd-MMM-yyyy"),
            ["Acquisition Price"] = e => e.AcquisitonPrice.ToString(),
            ["SR Issued"] = e => e.SRIssued.ToString(),
        };

        private int GetAccountId(Account acc) => acc.Id;

        /// <summary>
        /// The new account which is being edited
        /// </summary>
        private Account NewAccount { get; set; } = new Account();

        private bool ShowAccountForm { get; set; } = false;

        private bool AccountAddMode { get; set; } = true;

        #endregion

        #region Account Methods

        private void OnAccountEdit(int id)
        {
            AccountAddMode = false;
            ShowAccountForm = true;
            NewAccount = _dbContext.Accounts.Where(a => a.Id == id).FirstOrDefault() ?? new Account();
        }

        private List<Account> OnAccountDelete(int id)
        {
            Account? acc = _dbContext.Accounts.Where(a => a.Id == id).FirstOrDefault();
            if (acc == null)
                return TrustAccounts;

            _dbContext.Accounts.Remove(acc);
            _dbContext.SaveChanges();
            TrustAccounts = _dbContext.Accounts.ToList();
            StateHasChanged();
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
            if (ShouldAdd)
            {
                _dbContext.Accounts.Add(NewAccount);
            }
            else
            {
                _dbContext.Accounts.Update(NewAccount);
            }
            _dbContext.SaveChanges();
            TrustAccounts = _dbContext.Accounts.Where(e => e.TrustCode == NewEntry.TrustCode).ToList();
            CancelAccountForm();
            StateHasChanged();
        }

        private void CreateNewAccount()
        {
            NewAccount = new Account();
            NewAccount.TrustCode = NewEntry.TrustCode;
            NewAccount.TrustName = NewEntry.Trust_Name;
            Account? acc = _dbContext.Accounts.OrderByDescending(f => f.BorrowerCode).FirstOrDefault();
            NewAccount.BorrowerCode = acc == null ? 1 : acc.Id + 1;
            NewAccount.Assignor = NewEntry.SRHolder;
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
            TrustAccounts = new List<Account>();
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
            TrustAccounts = _dbContext.Accounts.Where(t => t.TrustCode == NewEntry.TrustCode).ToList();
            RecordData();
        }

        #endregion
    }
}
