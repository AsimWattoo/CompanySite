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

        #region Combined Trust Collection Properties

        private List<CombinedTrustCollection> CombinedCollections { get; set; } = new List<CombinedTrustCollection>();

        private Dictionary<string, Func<CombinedTrustCollection, string>> CombinedCollectionHeaders = new Dictionary<string, Func<CombinedTrustCollection, string>>()
        {
            ["Trust Name"] = p => p.Trust_Name,
            ["Total Collection"] = p => p.TotalCollection.ToString()
        };

        private int GetCombinedCollectionId(CombinedTrustCollection t) => t.Id;

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
            List<CollectionEntry> collections = _dbContext.Collections.ToList();
            collections.Where(f => f.Distribution_Id == null).GroupBy(t => t.TrustCode).ToList().ForEach(f =>
            {
                CollectionEntry? e = collections.Where(t => t.TrustCode == f.Key).FirstOrDefault();
                double totalCollection = 0;

                foreach(CollectionEntry collection in f)
                    totalCollection += collection.CreditAmount;

                CombinedCollections.Add(new CombinedTrustCollection() { Id = 0, Trust_Name = e.Trust_Name, TrustCode = f.Key, TotalCollection = totalCollection });
            });
        }

        #endregion

        #region Private Members

        private void Clear()
        {
            Model = new DistributionViewModel();
            ExpenseEntries = new List<ExpenseEntry>();
            Collections = new List<CollectionEntry>();
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
                TSFee = Model.TrusteeshipFee,
                FaceValue = Model.FaceValue,
            };
            _dbContext.DistributionEnteries.Add(e);
            _dbContext.SaveChanges();
            Collections.ForEach(c =>
            {
                c.Distribution_Id = e.Id;
                _dbContext.Collections.Update(c);
			});
            ExpenseEntries.ForEach(c => 
            {
                c.Distribution_Id = e.Id;
                _dbContext.Expenses.Update(c);
			});
            _dbContext.SaveChanges();
            Clear();
        }

        /// <summary>
        /// Fires when the combined trust collection row is selected from the database
        /// </summary>
        /// <param name="collection"></param>
        private void TotalCollectionRowSelected(CombinedTrustCollection collection)
        {
            Trust? t = _dbContext.Trusts.Where(t => t.TrustCode == collection.TrustCode).FirstOrDefault();
            if (t != null)
            {
                Model.TrustAge = t.TrustAge;
                Model.TrustSetupDate = t.TrustSetupDate.ToString("dd-MMM-yyyy");
                Model.Trust = t;
            }
            Model.TrusteeshipFeeLimit = t.TrusteeshipLimit;
            Model.TrustCode = collection.TrustCode;
            Model.TrustName = collection.Trust_Name;
            Model.TrusteeshipFeeBasis = t.TrusteeshipBasis;
            Model.ResolutionFeeBasis = t.ResolutionFeeBasis;
            Collections = _dbContext.Collections.Where(f => f.TrustCode == collection.TrustCode && f.Distribution_Id == null).ToList();
            //Getting expense enteries for the trust
            ExpenseEntries = _dbContext.Expenses.Where(e => e.TrustCode == collection.TrustCode && e.Distribution_Id == null).ToList();
            //Getting Previous distributions for the trust
            DistributionEntry? lastDistrubtion = _dbContext.DistributionEnteries.Where(t => t.Trust_Code == collection.TrustCode).OrderByDescending(t => t.Id).FirstOrDefault();

            if (lastDistrubtion != null)
                Model.OpeningBalance = lastDistrubtion.ClosingBalance;

            //Calculating total collections for the trust
            Model.TotalCollection = Collections.Where(e => e.TrustCode == collection.TrustCode).Sum(f => f.CreditAmount);

            //Calculating total other expenses
            Model.TotalOtherExpenses = ExpenseEntries.Sum(e => e.PaymentAmount);

            StateHasChanged();
        }

        /// <summary>
        /// Updates the table by removing the collection from list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<CollectionEntry> CollectionDeleted(int id)
        {
            CollectionEntry? collection = Collections.Where(c => c.Id == id).First();
            if (collection == null)
                return Collections;

            Collections.Remove(collection);
			Model.TotalCollection = Collections.Where(e => e.TrustCode == collection.TrustCode).Sum(f => f.CreditAmount);
            StateHasChanged();
            return Collections;
		}

        #endregion

    }
}
