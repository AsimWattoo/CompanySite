using Company_Site.Enum;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User
{
    public partial class DebtProfile : BaseAddPage<DebtProfileModel>,ITable<DebtProfileModel, int>
    {
        
        #region Public Properties

        public Dictionary<string, Func<DebtProfileModel, string>> Headers { get; set; } = new Dictionary<string, Func<DebtProfileModel, string>>()
        {
            ["Lender Name"] = p => p.Lender_Name,
            ["Facility"] = p => p.Facility,
            ["Account_No"] = p => p.Account_No.ToString(),
            ["POS"] = p => p.POS.ToString(),
            ["TOS"] = p => p.TOS,
            ["Interest_Rate"] = p => p.Interest_Rate.toString(),
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.DebtProfile;
            BaseHeaders = Headers;
        }

        #endregion

        #region Public Methods

        public int GetId(DebtProfileModel t) => t.Id;

        public bool SearchItem(DebtProfileModel e)
        {
            return e.Lender_Name.Contains(_text) || e.Facility.Contains(_text) || e.Account_No.Contains(_text) || e.POS.Contains(_text) || e.TOS.Contains(_text) || e.Interest_Rate.Contains(_text);
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
