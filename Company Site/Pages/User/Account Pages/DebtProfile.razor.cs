using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class DebtProfile : BaseModifierAddPage<DebtProfileModel>, ITable<DebtProfileModel, int>
    {

        #region Public Properties

        public Dictionary<string, Func<DebtProfileModel, string>> Headers { get; set; } = new Dictionary<string, Func<DebtProfileModel, string>>()
        {
            ["Lender Name"] = p => p.LenderName,
            ["Facility"] = p => p.Facility,
            ["Account_No"] = p => p.AccountNumber.ToString(),
            ["POS"] = p => p.POS.ToString(),
            ["TOS"] = p => p.TOS.ToString(),
            ["Interest_Rate"] = p => p.Res_InterestRate.ToString(),
        };

        #endregion

        #region Private Members

        private Account? userAccount;

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.DebtProfiles;
            userAccount = _dbContext.Accounts.Where(f => f.UserId == UserId.Value).FirstOrDefault();
        }

        protected override void SaveResetup()
        {
            NewEntry = new DebtProfileModel();
            if(userAccount != null)
                NewEntry.BorrowerCode = userAccount.BorrowerCode;
            RecordData();
        }

        #endregion

        #region Public Methods

        public int GetId(DebtProfileModel t) => t.Id;

        public bool SearchItem(DebtProfileModel e)
        {
            return e.LenderName.Contains(_text) || e.Facility.Contains(_text) || e.AccountNumber.ToString().Contains(_text) || e.POS.ToString().Contains(_text) || e.Res_InterestRate.ToString().Contains(_text);
        }

        private string _text;

        public List<DebtProfileModel> Search(List<DebtProfileModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        #endregion
    
    }
}
