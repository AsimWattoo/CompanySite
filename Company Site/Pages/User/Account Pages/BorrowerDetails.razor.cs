using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class BorrowerDetails : ComponentBase, ITable<BorrowerDetail, int>
    {
        #region Public Properties

        public List<BorrowerDetail> Enteries { get; set; } = new List<BorrowerDetail>();

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

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ProtectedSessionStorage _storage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Enteries = _dbContext.BorrowerDetails.ToList();
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

        public void EditRecord(int id)
        {
            _storage.SetAsync("BorrowerPageMode", "edit");
            _storage.SetAsync("BorrowerId", id);
            _navigationManager.NavigateTo("/borrowerdetails/add");
        }

        public int GetId(BorrowerDetail t) => t.Id;

        public List<string> GetTableRows(BorrowerDetail record)
        {
            return new List<string>()
            {
                record.Name,
                record.Position,
                record.Net_Worth,
                record.NumberOfShares.ToString(),
                record.PercentOfShareHeld.ToString(),
                record.Wilful_Defaulter,
            };
        }

        public List<BorrowerDetail> Search(List<BorrowerDetail> enteries, string text)
        {
            return enteries.Where(e => e.Name.Contains(text) || e.Position.Contains(text) || e.Net_Worth.Contains(text) || e.Wilful_Defaulter.Contains(text)).ToList();
        }

        #endregion

        #region Private Methods

        private void GoToAddPage()
        {
            _storage.SetAsync("BorrowerMode", "add");
            _navigationManager.NavigateTo("/borrowerdetails/add");
        }

        #endregion
    }
}
