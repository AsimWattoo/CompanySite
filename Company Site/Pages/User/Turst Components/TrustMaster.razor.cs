using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Turst_Components
{

    public partial class TrustMaster : BaseAddPage<Trust>, ITable<Trust, int>
    {
        #region Private Members

        public Dictionary<string, Func<Trust, string>> Headers { get; set; } = new Dictionary<string, Func<Trust, string>>()
        {
            ["Trust Date"] = (Trust e) => e.TrustSetupDate.ToString("dd/mm/yyyy"),
            ["SR Holder"] = (Trust e) => e.SRHolder,
            ["Ratio"] = (Trust e) => e.Ratio.ToString(),
            ["Upside"] = (Trust e) => e.IssuerUpsideShare.ToString(),
            ["Setup Date"] = (Trust e) => e.SetupDate.ToString("dd/mm/yyyy"),
            ["SR Issued"] = (Trust e) => e.SRIssued.ToString(),
            ["SR O/s"] = (Trust e) => e.SROs,
            ["Turst Age"] = (Trust e) => e.TrustAge.ToString(),
            ["Trusteeship Fee"] = (Trust e) => e.TrusteeFee.ToString(),
            ["Resolution Fee"] = (Trust e) => e.ResolutionFee.ToString(),
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.Trusts;
            BaseHeaders = Headers;
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
                t.TrustSetupDate.ToString("dd/MM/yyyy"),
                t.SRHolder,
                Math.Round(t.Ratio, 2).ToString(),
                t.IssuerUpsideShare.ToString(),
                t.SetupDate.ToString("dd/MM/yyyy"),
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
