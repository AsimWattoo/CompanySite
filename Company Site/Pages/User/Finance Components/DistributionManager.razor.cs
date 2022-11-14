using Company_Site.Data;
using Company_Site.DB;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;

using MimeKit.Cryptography;

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

        private void Clear()
        {
            Model = new DistributionViewModel();
            ExpenseEntries = new List<ExpenseEntry>();
        }

        /// <summary>
        /// Saves the distribution in the table
        /// </summary>
        private void Process()
        {
            DistributionEntry e = new DistributionEntry()
            {
                Trust_Code = Model.TrustCode,
                Trust_Name = Model.TrustName,
                Adjustment = Model.OtherAdjustments,
                Advance_TDS_Decimal = Model.LessAdvancedTDS,
                Balance = Model.Balance,
                CFee = Model.CollectionFee,
                CLFV = Model.TotalClosingFVSRValue,
                ClosingBalance = Model.ClosingBalance,
                Collections = Model.TotalCollection,
                NormalDist = Model.TotalSRValue,
                Opening = Model.OpeningBalance,
                OPFV = Model.TotalOpeningFVSRValue,
                OtherExpense = Model.TotalOtherExpenses,
                Provision = Model.LessProvision,
                RFees = Model.ResolutionFee,
                SRIssued = (int)Model.SRIssued,
                //TODO: Fill these values
                //Distribution_AMT =
                //SROutStanding =
                Surplus = Model.TotalUpsideSharePercentage,
                TDS_Decimal = Model.TDS,
                TSFee = Model.TrusteeshipFee
            };
            _dbContext.DistributionEnteries.Add(e);
            _dbContext.SaveChanges();
        }

        private void CollectionRowSelected(CollectionEntry collection)
        {
            Trust? t = _dbContext.Trusts.Where(t => t.TrustCode == collection.TrustCode).FirstOrDefault();
            if(t != null)
            {
                Model.TrustAge = t.TrustAge;
                Model.TrustSetupDate = t.TrustSetupDate.ToString("dd-MMM-yyyy");
                Model.Trust = t;
            }
            Model.TrustCode = collection.TrustCode;
            Model.TrustName = collection.Trust_Name;
            //Getting expense enteries for the trust
            ExpenseEntries = _dbContext.Expenses.Where(e => e.TrustCode == collection.TrustCode).ToList();
            //Getting Previous distributions for the trust
            DistributionEntry? lastDistrubtion = _dbContext.DistributionEnteries.Where(t => t.Trust_Code == collection.TrustCode).OrderByDescending(t => t.Id).FirstOrDefault();

            if(lastDistrubtion != null)
                Model.OpeningBalance = lastDistrubtion.ClosingBalance;

            //Calculating total collections for the trust
            Model.TotalCollection = Collections.Where(e => e.TrustCode == collection.TrustCode).Sum(f => f.CreditAmount);

            //Calculating total other expenses
            Model.TotalOtherExpenses = ExpenseEntries.Sum(e => e.PaymentAmount);

            StateHasChanged();
        }

        #endregion

    }
}
