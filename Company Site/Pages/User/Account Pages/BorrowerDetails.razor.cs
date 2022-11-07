using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class BorrowerDetails : BasePage<BorrowerDetail>, ITable<BorrowerDetail, int>
    {
        #region Public Properties

        public Dictionary<string, Func<BorrowerDetail, dynamic>> Headers { get; set; } = new Dictionary<string, Func<BorrowerDetail, dynamic>>()
        {
            ["Name"] = p => p.Name,
            ["Position"] = p => p.Position,
            ["Net_Worth"] = p => p.Net_Worth,
            ["Number of shares held"] = p => p.NumberOfShares.ToString(),
            ["Percent of share hold"] = p => p.PercentOfShareHeld,
            ["Will Full Defaulter"] = p => p.Wilful_Defaulter,
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            Enteries = _dbContext.BorrowerDetails.ToList();
            PageModeKey = "BorrowerPageMode";
            IdKey = "BorrowerId";
            AddPageUrl = "/borrowerdetails/add";
        }

        #endregion

        #region Public Methods

        public List<BorrowerDetail> DeleteRecord(int id)
        {
            _dbContext.BorrowerDetails.Remove(_dbContext.BorrowerDetails.Where(f => f.Id == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbContext.BorrowerDetails.ToList();
            return Enteries;
        }

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
