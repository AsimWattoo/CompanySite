using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Company_Site.Pages.User
{
    public partial class StatementOfAccount : ComponentBase
    {

        #region Main Table Fields

        /// <summary>
        /// The list of statements for an account
        /// </summary>
        private List<StatementOfAccountViewModel> Statements { get; set; } = new List<StatementOfAccountViewModel>();

        public Dictionary<string, Func<StatementOfAccountViewModel, string>> Headers { get; set; } = new Dictionary<string, Func<StatementOfAccountViewModel, string>>()
        {
            ["Trust Code"] = p => p.Trust_Code,
            ["Borrower Code"] = p => p.Borrower_Code.ToString(),
            ["Trust Name"] = p => p.Trust_Name,
            ["Account Number"] = p => p.Account_Number.ToString(),
            ["Account Name"] = p => p.Account_Name,
            ["Bank Name"] = p => p.Bank_Name,
            ["Status"] = p => p.StatusAs,
            ["Facility"] = p => p.Facility,
            ["Date"] = p => p.SOADate.ToString("dd-MMM-yyyy"),
            ["Opening Balance"] = p => p.Opening_Balance.ToString(),
            ["Interest"] = p => p.Interest.ToString(),
            ["Penal Interest (Rs.)"] = p => p.Penal_Interest.ToString(),
            ["Penal Percent (%)"] = p => $"{p.Penal_Percent}%",
            ["Frequency"] = p => p.Frequency,
            ["Calculation"] = p => p.Calculation,
            ["Note"] = p => p.Note,
            ["Closed Date"] = p => p.ClosedDate.ToString("dd-MMM-yyyy"),
            ["Maker"] = p => p.Maker,
            ["Checker"] = p => p.Checker,
        };

        #endregion

        #region Expenses Table Fields

        private Dictionary<string, Func<ExpenseEntry, string>> ExpenseHeaders { get; set; } = new Dictionary<string, Func<ExpenseEntry, string>>()
        {
            ["Trust Code"] = (ExpenseEntry e) => e.TrustCode,
            ["Borrower Code"] = (ExpenseEntry e) => e.Borrower_Code.ToString(),
            ["Trust Name"] = (ExpenseEntry e) => e.Trust_Name,
            ["Vendor"] = (ExpenseEntry e) => e.Vendor_Name,
            ["Service"] = (ExpenseEntry e) => e.Service,
            ["Amount"] = (ExpenseEntry e) => e.BillAmount.ToString(),
            ["Payment Date"] = (ExpenseEntry e) => e.PaymentDate.ToString("dd-MMM-yyyy"),
        };

        /// <summary>
        /// List of expense enteries
        /// </summary>
        private List<ExpenseEntry> ExpenseEntries = new List<ExpenseEntry>();

        public int GetExpenseId(ExpenseEntry e) => e.Id;

        #endregion

        #region Collection Table Fields

        public Dictionary<string, Func<CollectionEntry, string>> CollectionHeaders { get; set; } = new Dictionary<string, Func<CollectionEntry, string>>()
        {
            ["Trust Code"] = (CollectionEntry e) => e.TrustCode,
            ["Borrower"] = (CollectionEntry e) => e.Borrower.ToString(),
            ["Trust Name"] = (CollectionEntry e) => e.Trust_Name,
            ["Borrower Name"] = (CollectionEntry e) => e.BorrowerName,
            ["Amount"] = (CollectionEntry e) => e.CreditAmount.ToString(),
            ["Credit Date"] = (CollectionEntry e) => e.CreditDate.ToString("dd-MMM-yyyy"),
            ["Source"] = (CollectionEntry e) => e.Source,
        };

        public List<CollectionEntry> CollectionEntries { get; set; } = new List<CollectionEntry>();

        private int GetCollectionId(CollectionEntry e) => e.Id;

        #endregion

        #region Distribution Table Fields

        public Dictionary<string, Func<DistributionEntry, string>> DistributionHeaders { get; set; } = new Dictionary<string, Func<DistributionEntry, string>>()
        {
            ["Trust Code"] = e => e.Trust_Code,
            ["Trust Name"] = e => e.Trust_Name,
            ["SR Issued"] = e => e.SRIssued.ToString(),
            ["SR Outstanding"] = e => e.SROutStanding.ToString(),
            ["Opening"] = e => Number.Round(e.Opening),
            ["Collections"] = e => Number.Round(e.Collections),
            ["TS Fee"] = e => Number.Round(e.TSFee),
            ["RS Fee"] = e => Number.Round(e.RFees),
            ["C Fee"] = e => Number.Round(e.CFee),
            ["Other Expenses"] = e => Number.Round(e.OtherExpense),
            ["Adjustments"] = e => Number.Round(e.Adjustment),
            ["Balance"] = e => Number.Round(e.Balance),
            ["TDS Decimal"] = e => Number.Round(e.TDS_Decimal),
            ["Advance TDS Decimal"] = e => Number.Round(e.Advance_TDS_Decimal),
            ["Disstribution AMT"] = e => Number.Round(e.Distribution_AMT),
            ["Normal Dist"] = e => Number.Round(e.NormalDist),
            ["Surplus"] = e => Number.Round(e.Surplus),
            ["Provision"] = e => Number.Round(e.Provision),
            ["Closing Balance"] = e => Number.Round(e.ClosingBalance),
            ["OPFV"] = e => Number.Round(e.OPFV),
            ["CLFv"] = e => Number.Round(e.CLFV),
        };

        public List<DistributionEntry> DistributionEntries { get; set; } = new List<DistributionEntry>();

        private int GetDistributionId(DistributionEntry entry) => entry.Id;

        #endregion

        #region Private Members

        /// <summary>
        /// The model currently selected
        /// </summary>
        private StatementOfAccountViewModel Model { get; set; } = new StatementOfAccountViewModel();

        private StatementOfAccountMode Mode { get; set; } = StatementOfAccountMode.Collections;

        /// <summary>
        /// The mode of the page for adding
        /// </summary>
        private StatementOfAccountMode AddMode { get; set; }

        #endregion

        #region Form Fields

        public DateTime Date { get; set; } = DateTime.Now;

        public double Amount { get; set; } = 0;

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ApplicationState _applicationState { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the component is initialized
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            //TODO: Resolve Trust Code issue here.
            //Loading Account Data to Statement of Account View Model
            //List<Account> accounts = new List<Account>();
            //List<Trust> trusts = await _dbContext.Trusts.ToListAsync();
            //List<DebtProfileModel> debtProfiles = await _dbContext.DebtProfiles.ToListAsync();
            //List<DistributionEntry> distributionEntries = await _dbContext.DistributionEnteries.ToListAsync();
            //foreach (Account acc in accounts)
            //{
            //    Trust? t = trusts.Where(f => f.TrustCode == acc.TrustCode).First();
            //    DebtProfileModel? debt = debtProfiles.Where(f => f.BorrowerCode == acc.BorrowerCode).FirstOrDefault();
            //    DistributionEntry? trustDis = distributionEntries.Where(t => t.Trust_Code == acc.TrustCode).OrderByDescending(f => f.Id).FirstOrDefault();

            //    if (t == null || debt == null)
            //        continue;

            //    double opening = trustDis == null ? 0 : trustDis.ClosingBalance;

            //    Statements.Add(new StatementOfAccountViewModel()
            //    {
            //        Account_Number = debt.AccountNumber,
            //        Borrower_Code = acc.BorrowerCode,
            //        Account_Name = acc.Company,
            //        Bank_Name = acc.Assignor,
            //        Trust_Code = acc.TrustCode,
            //        Trust_Name = t.Trust_Name,
            //        Calculation = debt.Calculation,
            //        ClosedDate = debt.Res_EndDate,
            //        Facility = debt.Facility,
            //        Frequency = debt.Facility,
            //        Note = debt.note,
            //        Opening_Balance = opening,
            //        Penal_Interest = debt.PenalAmount,
            //        SOADate = DateTime.Now,
            //        Interest = debt.BaseInterestRate + debt.Spread,
            //        StatusAs = debt.Status,
            //        Maker = debt.CreatorName,
            //        Checker = debt.Modifier,
            //        Penal_Percent = debt.PenalInterest,
            //    });
            //}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires when the statement of row item is clicked
        /// </summary>
        /// <param name="model"></param>
        private void StatementRowClicked(StatementOfAccountViewModel model)
        {
            Model = model;
            ExpenseEntries = _dbContext.Expenses.Where(e => e.Borrower_Code == model.Borrower_Code).ToList();
            StateHasChanged();
        }

        /// <summary>
        /// Changes the mode of the statement of account
        /// </summary>
        /// <param name="model"></param>
        private void ChangeMode(StatementOfAccountMode mode)
        {
            Mode = mode;
        }

        #endregion

        #region Public Methods

        public int GetId(StatementOfAccountViewModel t) => t.Borrower_Code;

        #endregion

    }
}
