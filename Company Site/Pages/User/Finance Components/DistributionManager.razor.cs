using Company_Site.Data;
using Company_Site.DB;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class DistributionManager : ComponentBase
    {

        #region Collection Properties

        private List<CollectionEntry> Collections = new List<CollectionEntry>();

        private Dictionary<string, Func<CollectionEntry, string>> CollectionHeaders { get; set; } = new Dictionary<string, Func<CollectionEntry, string>>()
        {
            ["Date"] = p => p.CreditDate.ToString("dd-MM-yyyy"),
            ["Trust Code"] = p => p.TrustCode,
            ["Borrower"] = p => p.BorrowerName,
            ["Amount"] = p => p.CreditAmount.ToString(),
        };

        private int GetCollectionId(CollectionEntry c) => c.Id;

        #endregion

        #region Expense Properties

        private List<ExpenseEntry> ExpenseEntries { get; set; } = new List<ExpenseEntry>();

        private Dictionary<string, Func<ExpenseEntry, string>> ExpenseHeaders { get; set; } = new Dictionary<string, Func<ExpenseEntry, string>>()
        {
            ["Date"] = e => e.PaymentDate.ToString("dd-MMM-yyyy"),
            ["Head"] = e => e.Service,
            //TODO: No Description or Note fields in the Expense manager tabke
            ["Description"] = e => "",
            ["Distribution Date"] = e => ""
        };

        private int GetExpenseId(ExpenseEntry e) => e.Id;

        #endregion

        #region Private Properties

        /// <summary>
        /// The model for the distribution page
        /// </summary>
        private DistributionViewModel Model { get; set; } = new DistributionViewModel();

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            //Loading Collections Data
            Collections = _dbContext.Collections.ToList();
        }

        #endregion

        #region Private Members

        private void CollectionRowSelected(CollectionEntry collection)
        {
            Trust t = _dbContext.Trusts.Where(t => t.TrustCode == collection.TrustCode).FirstOrDefault();
            if(t != null)
            {
                Model.TrustAge = t.TrustAge;
                Model.TrustSetupDate = t.TrustSetupDate.ToString("dd-MMM-yyyy");
                Model.Trust = t;
            }
            Model.TrustCode = collection.TrustCode;
            Model.TrustName = collection.Trust_Name;
            ExpenseEntries = _dbContext.Expenses.Where(e => e.TrustCode == collection.TrustCode).ToList();
            StateHasChanged();
        }

        #endregion

    }
}
