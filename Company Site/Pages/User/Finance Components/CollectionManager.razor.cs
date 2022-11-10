using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class CollectionManager : BaseAddPage<CollectionEntry>, ITable<CollectionEntry, int>
    {
        #region Private Members

        public Dictionary<string, Func<CollectionEntry, string>> Headers { get; set; } = new Dictionary<string, Func<CollectionEntry, string>>()
        {
            ["Trust Code"] = (CollectionEntry e) => e.TrustCode,
            ["Borrower"] = (CollectionEntry e) => e.Borrower.ToString(),
            ["Trust Name"] = (CollectionEntry e) => e.Trust_Name,
            ["Borrower Name"] = (CollectionEntry e) => e.BorrowerName,
            ["Amount"] = (CollectionEntry e) => e.CreditAmount.ToString(),
            ["Credit Date"] = (CollectionEntry e) => e.CreditDate.ToString("dd-MMM-yyyy"),
            ["Source"] = (CollectionEntry e) => e.Source,
        };

        /// <summary>
        /// The list of borrowers to show
        /// </summary>
        private List<BorrowerDetail> Borrowers { get; set; } = new List<BorrowerDetail>();

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.Collections;
            Borrowers = _dbContext.BorrowerDetails.ToList();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets and sets the name of the borrower based on id
        /// </summary>
        /// <param name="value"></param>
        private void BorrowerChanged(int value)
        {
            NewEntry.Borrower = value;
            NewEntry.BorrowerName = Borrowers.Where(f => f.Id == value).FirstOrDefault()?.Name;
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(CollectionEntry e) => e.Id;

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<CollectionEntry> Search(List<CollectionEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Source.Equals(text) || e.Trust_Name.Equals(text) || e.Borrower.Equals(text) || e.BorrowerName.Equals(text)).ToList();
        }

        #endregion
    }
}
