using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class AccountNet : BaseAddPage<AccountNetModel, string>, ITable<AccountNetModel, string>
    {
        #region Public Properties

        public Dictionary<string, Func<AccountNetModel, string>> Headers { get; set; } = new Dictionary<string, Func<AccountNetModel, string>>()
        {
            ["CaseTitle"] = p => p.CaseTitle,
            ["Forum"] = p => p.Forum,
            ["Status"] = p => p.Status,
            ["Next Date"] = p => p.NextDate.ToString("dd-MMM-yyyy"),
            ["Order/Outcome"] = p => p.FinalOrder,
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.AccountNets;
        }

        protected override void LoadData()
        {
            Enteries = _dbSet.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        #endregion

        #region Public Methods

        public string GetId(AccountNetModel t) => t.Id;

        public bool SearchItem(AccountNetModel e)
        {
            return e.CaseTitle.Contains(_text) || e.Forum.Contains(_text) || e.Status.Contains(_text) || e.FinalOrder.Contains(_text);
        }

        private string _text;

        public List<AccountNetModel> Search(List<AccountNetModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        protected override bool SaveSetup()
        {
            if (_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please Select an Account in the account page");
                return false;
            }
            else
                return base.SaveSetup();
        }

        #endregion
    }
}
