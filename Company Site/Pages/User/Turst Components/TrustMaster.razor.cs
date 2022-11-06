using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Turst_Components
{

    public partial class TrustMaster : ComponentBase, ITable<Trust, int>
    {
        #region Private Members

        /// <summary>
        /// The expenses to be shown
        /// </summary>
        public List<Trust> Enteries { get; set; } = new List<Trust>();

        public Dictionary<string, Func<Trust, dynamic>> Headers { get; set; } = new Dictionary<string, Func<Trust, dynamic>>()
        {
            ["Trust Date"] = (Trust e) => e.TrustSetupDate.ToString("dd/mm/yyyy"),
            ["SR Holder"] = (Trust e) => e.SRHolder,
            ["Ratio"] = (Trust e) => e.Ratio,
            ["Upside"] = (Trust e) => e.IssuerUpsideShare.ToString(),
            ["Setup Date"] = (Trust e) => e.SetupDate.ToString("dd/mm/yyyy"),
            ["SR Issued"] = (Trust e) => e.SRIssued.ToString(),
            ["SR O/s"] = (Trust e) => e.SROs,
            ["Turst Age"] = (Trust e) => e.TrustAge.ToString(),
            ["Trusteeship Fee"] = (Trust e) => e.TrusteeFee.ToString(),
            ["Resolution Fee"] = (Trust e) => e.ResolutionFee.ToString(),
        };

        #endregion

        #region Injected Members

        [Inject]
        private ProtectedSessionStorage _sessionStorage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// After the component is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Enteries = _dbContext.Trusts.ToList();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the add page
        /// </summary>
        private void GoToAddPage()
        {
            _sessionStorage.SetAsync("TrustPageMode", "add");
            _navigationManager.NavigateTo("/trustmaster/add");
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(Trust e) => e.Id;

        /// <summary>
        /// Deletes the expense entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Trust> DeleteRecord(int id)
        {
            _dbContext.Trusts.Remove(_dbContext.Trusts.Where(t => t.Id == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbContext.Trusts.ToList();
            return Enteries;
        }

        /// <summary>
        /// Edits the record
        /// </summary>
        /// <param name="id"></param>
        public void EditRecord(int id)
        {
            _sessionStorage.SetAsync("TrustPageMode", "edit");
            _sessionStorage.SetAsync("TrustId", id);
            _navigationManager.NavigateTo("/trustmaster/add");
        }

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<Trust> Search(List<Trust> enteries, string text)
        {
            text = text.ToLower();
            return enteries.Where(e => e.TrustCode.Contains(text) || e.SRHolder.Contains(text) || e.SetupDate.ToString().Contains(text) || e.TrusteeFee.ToString().Contains(text) || e.IssuerShare.ToString().Contains(text) || e.HolderShare.ToString().Contains(text)).ToList();
        }

        /// <summary>
        /// Specifies each table row
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public List<string> GetTableRows(Trust t)
        {
            return new List<string>()
            {
                t.TrustSetupDate.ToString("dd/mm/yyyy"),
                t.SRHolder,
                Math.Round(t.Ratio, 2).ToString(),
                t.IssuerUpsideShare.ToString(),
                t.SetupDate.ToString("dd/mm/yyyy"),
                t.SRIssued.ToString(),
                t.SROs,
                t.TrustAge.ToString(),
                t.TrusteeFee.ToString(),
                t.ResolutionFee.ToString(),
            };
        }

        #endregion
    }
}
