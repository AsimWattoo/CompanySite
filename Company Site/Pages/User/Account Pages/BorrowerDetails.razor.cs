using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class BorrowerDetails : BaseAddPage<BorrowerDetail>, ITable<BorrowerDetail, int>
    {
        #region Public Properties

        public Dictionary<string, Func<BorrowerDetail, string>> Headers { get; set; } = new Dictionary<string, Func<BorrowerDetail, string>>()
        {
            ["Name"] = p => p.Name,
            ["Position"] = p => p.Position,
            ["Net_Worth"] = p => p.Net_Worth,
            ["Number of shares held"] = p => p.NumberOfShares.ToString(),
            ["Percent of share hold"] = p => p.PercentOfShareHeld.ToString(),
            ["Will Full Defaulter"] = p => p.Wilful_Defaulter,
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.BorrowerDetails;
        }

        #endregion

        #region Public Methods

        public int GetId(BorrowerDetail t) => t.Id;

        public bool SearchItem(BorrowerDetail e)
        {
            return e.Name.Contains(_text) || e.Position.Contains(_text) || e.Wilful_Defaulter.Contains(_text);
        }

        private string _text;

        public List<BorrowerDetail> Search(List<BorrowerDetail> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        #endregion
    }
}
