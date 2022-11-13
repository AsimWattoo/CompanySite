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
        private List<Account> Borrowers { get; set; } = new List<Account>();

        /// <summary>
        /// Headers for the relation table header
        /// </summary>
        private Dictionary<string, Func<TrustRelationModel, string>> RelationTableHeaders{ get; set; } = new Dictionary<string, Func<TrustRelationModel, string>>()
        {
            ["Trust Code"] = t => t.TrustCode,
            ["Trust Name"] = t => t.Trust == null ? "" : t.Trust.Trust_Name,
            ["Holder Name"] = t => t.Trust == null ? "" : t.Trust?.SRHolder,
            ["Share"] = t => t.Share.ToString(),
            ["Amount"] = t => t.Amount.ToString()
        };

        private List<TrustRelationModel> TrustRelationModelEnteries { get; set; } = new List<TrustRelationModel>();

        private Dictionary<string, TableInput<TrustRelationModel>> RelationModelEditableFields { get; set; } = new Dictionary<string, TableInput<TrustRelationModel>>()
        {
            ["Trust Code"] = null,
            ["Trust Name"] = null,
            ["Holder Name"] = null,
            ["Share"] = new (t => nameof(t.Share), 0),
            ["Amount"] = new (t => nameof(t.Amount), 0),
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.Collections;
            Borrowers = _dbContext.Accounts.ToList();
            TrustRelationModelEnteries.ForEach(f =>
            {
                f.Account = new Account();
                f.Trust = new Trust();
            });
        }

        #endregion

        #region Private Methods

        private int GetRelationId(TrustRelationModel model) => model.Id;

        /// <summary>
        /// Gets and sets the name of the borrower based on id
        /// </summary>
        /// <param name="value"></param>
        private void BorrowerChanged(int value)
        {
            NewEntry.Borrower = value;
            NewEntry.BorrowerName = Borrowers.Where(f => f.Id == value).FirstOrDefault()?.Company_Name;
            TrustRelationModelEnteries = _dbContext.TrustRelations.Where(f => f.BorrowerCode == NewEntry.Borrower).ToList();
            TrustRelationModelEnteries.ForEach(f => f.Populate(_dbContext));
        }

        protected override bool SaveSetup()
        {
            if (base.SaveSetup())
            {
                foreach (TrustRelationModel model in TrustRelationModelEnteries)
                {
                    _dbContext.TrustRelations.Update(model);
                }
                return true;
            }
            else
                return false;
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

        public override void OnEdit(int id)
        {
            TrustRelationModelEnteries = _dbContext.TrustRelations.Where(f => f.BorrowerCode == NewEntry.Borrower).ToList();
        }

        protected override void SaveResetup()
        {
            NewEntry = new CollectionEntry();
            TrustRelationModelEnteries = new List<TrustRelationModel>();
        }

        #endregion
    }
}
