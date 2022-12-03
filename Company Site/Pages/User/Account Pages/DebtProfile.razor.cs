using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class DebtProfile : BaseModifierAddPage<DebtProfileModel, int>, ITable<DebtProfileModel, int>
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

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.DebtProfiles;
        }

        protected override bool SaveSetup()
        {
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account before adding a debt profile");
                return false;
            }
            return base.SaveSetup();
        }

        protected override void SaveResetup()
        {
            NewEntry = new DebtProfileModel();
            if(_applicationState.BorrowerCode != -1)
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
            RecordData();
        }

        protected override void LoadData()
        {
            Enteries = _dbSet.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
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
