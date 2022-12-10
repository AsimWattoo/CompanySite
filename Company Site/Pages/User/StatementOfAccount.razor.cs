using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

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
        /// Indicates whether to add a record or edit
        /// </summary>
        private bool ShouldAddExpense = true;

        /// <summary>
        /// List of expense enteries
        /// </summary>
        private List<ExpenseEntry> ExpenseEntries = new List<ExpenseEntry>();

        private int GetExpenseId(ExpenseEntry e) => e.Id;

        private ExpenseEntry? SelectedExpenseEntry { get; set; } = null;

        private void ExpenseRowClicked(ExpenseEntry e)
        {
            SelectedExpenseEntry = e;
            NewEntry = StatementOfAccountDataViewModel.FromExpense(e);
            ShouldAddExpense = false;
            StateHasChanged();
        }

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

        private CollectionEntry? SelectedCollectionEntry { get; set; }

        private void CollectionRowClicked(CollectionEntry e)
        {
            SelectedCollectionEntry = e;
            NewEntry = StatementOfAccountDataViewModel.FromCollection(e);
            ShouldAddCollection = false;
            StateHasChanged();
        }

        /// <summary>
        /// Indicates whether to add a record or edit
        /// </summary>
        private bool ShouldAddCollection = true;

        #endregion

        #region Disbursment Table Fields

        public List<DisburmentModel> DisburmentEntries { get; set; } = new List<DisburmentModel>();

        public Dictionary<string, Func<DisburmentModel, string>> DisbursmentHeaders { get; set; } = new Dictionary<string, Func<DisburmentModel, string>>()
        {
            ["Date"] = e => e.Date.ToString("dd-MMM-yyyy"),
            ["Amount"] = e => Number.Round(e.Amount),
            ["Adjust Towards"] = e => e.AdjustTowards,
            ["Note"] = e => e.Note
        };

        private int GetDisbursmentId(DisburmentModel e) => e.Id;

        private DisburmentModel? SelectedDisbursementEntry { get; set; }

        private void DisbursementRowClicked(DisburmentModel e)
        {
            SelectedDisbursementEntry = e;
            NewEntry = StatementOfAccountDataViewModel.FromDisbursment(e);
            ShouldAddDisbursment = false;
            StateHasChanged();
        }

        /// <summary>
        /// Indicates whether to add a record or edit
        /// </summary>
        private bool ShouldAddDisbursment = true;

        #endregion

        #region Interest Rate Change Entries

        private List<InterestRateChangeModel> InterestRateChangeEntries { get; set; } = new List<InterestRateChangeModel>();

        private Dictionary<string, Func<InterestRateChangeModel, string>> InterestRateChangeHeaders { get; set; } = new Dictionary<string, Func<InterestRateChangeModel, string>>()
        {
            ["Date"] = e => e.Date.ToString("dd-MMM-yyyy"),
            ["Revised Interest Rate"] = e => Number.Round(e.RevisedInterestRate) + "%",
            ["Note"] = e => e.Note
        };

        private int GetInterestRateChangeId(InterestRateChangeModel e) => e.Id;

        private InterestRateChangeModel? SelectedInterestRateEntry { get; set; }

        private void InterestRateRowClicked(InterestRateChangeModel e)
        {
            SelectedInterestRateEntry = e;
            NewEntry = StatementOfAccountDataViewModel.FromInterestRate(e);
            ShouldAddInterestRate = false;
            StateHasChanged();
        }

        private bool ShouldAddInterestRate = true;

        #endregion

        #region Private Members

        /// <summary>
        /// The model currently selected
        /// </summary>
        private StatementOfAccountViewModel Model { get; set; } = new StatementOfAccountViewModel();

        private StatementOfAccountMode Mode { get; set; } = StatementOfAccountMode.Collections;

        private StatementOfAccountDataViewModel NewEntry { get; set; } = new StatementOfAccountDataViewModel();

        private List<string> _errors = new List<string>();

        private bool DeleteConfirmationModalShown { get; set; } = false;

        /// <summary>
        /// The list of debt profile models
        /// </summary>
        private List<DebtProfileModel> DebtProfiles { get; set; } = new List<DebtProfileModel>();

        /// <summary>
        /// The account for filtering
        /// </summary>
        private string? Account { get; set; } = "all";

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
            //Loading Account Data to Statement of Account View Model
            List<TrustRelationModel> relations = _dbContext.TrustRelations.ToList();
            List<Account> accounts = _dbContext.Accounts.ToList();
            List<Trust> trusts = await _dbContext.Trusts.ToListAsync();
            List<DebtProfileModel> debtProfiles = await _dbContext.DebtProfiles.ToListAsync();
            List<DistributionEntry> distributionEntries = await _dbContext.DistributionEnteries.ToListAsync();
            foreach (TrustRelationModel relation in relations)
            {
                Account acc = accounts.Where(f => f.BorrowerCode == relation.BorrowerCode).First();
                Trust? t = trusts.Where(f => f.TrustCode == relation.TrustCode).First();
                DebtProfileModel? debt = debtProfiles.Where(f => f.BorrowerCode == acc.BorrowerCode).FirstOrDefault();
                DistributionEntry? trustDis = distributionEntries.Where(t => t.Trust_Code == relation.TrustCode).OrderByDescending(f => f.Id).FirstOrDefault();

                if (t == null || debt == null)
                    continue;

                double opening = trustDis == null ? 0 : trustDis.ClosingBalance;

                Statements.Add(new StatementOfAccountViewModel()
                {
                    Account_Number = debt.AccountNumber,
                    Borrower_Code = acc.BorrowerCode,
                    Account_Name = acc.Company,
                    Bank_Name = acc.Assignor,
                    Trust_Code = t.TrustCode,
                    Trust_Name = t.Trust_Name,
                    Calculation = debt.Calculation,
                    ClosedDate = debt.Res_EndDate,
                    Facility = debt.Facility,
                    Frequency = debt.Facility,
                    Note = debt.note,
                    Opening_Balance = opening,
                    Penal_Interest = debt.PenalAmount,
                    SOADate = DateTime.Now,
                    Interest = debt.BaseInterestRate + debt.Spread,
                    StatusAs = debt.Status,
                    Maker = debt.CreatorName,
                    Checker = debt.Modifier,
                    Penal_Percent = debt.PenalInterest,
                });
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires when the account is changed
        /// </summary>
        /// <param name="value"></param>
        private void AccountChanged(string value)
        {
            Account = value;
            UpdateEntries(Model, StatementOfAccountMode.Collections, forceUpdate: true);
            UpdateEntries(Model, StatementOfAccountMode.Expenses, forceUpdate: true);
            UpdateEntries(Model, StatementOfAccountMode.Disbursement, forceUpdate: true);
            UpdateEntries(Model, StatementOfAccountMode.Interest, forceUpdate: true);
        }

        /// <summary>
        /// Fires when the statement of row item is clicked
        /// </summary>
        /// <param name="model"></param>
        private void StatementRowClicked(StatementOfAccountViewModel model)
        {
            Model = model;
            DebtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == Model.Borrower_Code).ToList();
            ClearEntries();
            UpdateEntries(model, StatementOfAccountMode.Collections);
            UpdateEntries(model, StatementOfAccountMode.Expenses);
            UpdateEntries(model, StatementOfAccountMode.Disbursement);
            UpdateEntries(model, StatementOfAccountMode.Interest, forceUpdate: true);
            StateHasChanged();
        }

        private void UpdateEntries(StatementOfAccountViewModel model, StatementOfAccountMode mode, bool forceUpdate = false)
        {
            switch (mode)
            {
                case StatementOfAccountMode.Expenses:
                    ExpenseEntries = Account == "all" ? _dbContext.Expenses.Where(e => e.Borrower_Code == model.Borrower_Code && e.TrustCode == model.Trust_Code).ToList() : _dbContext.Expenses.Where(e => e.Borrower_Code == model.Borrower_Code && e.TrustCode == model.Trust_Code && (e.AdjustTowards == "prop" || e.AdjustTowards.Equals(Account))).ToList();
                    break;
                case StatementOfAccountMode.Collections:
                    CollectionEntries = Account == "all" ? _dbContext.Collections.Where(e => e.TrustCode == model.Trust_Code && e.Borrower == model.Borrower_Code).ToList() : _dbContext.Collections.Where(e => e.TrustCode == model.Trust_Code && e.Borrower == model.Borrower_Code && (e.Adjustment == "prop" || e.Adjustment.Equals(Account))).ToList();
                    break;
                case StatementOfAccountMode.Disbursement:
                    DisburmentEntries = Account == "all" ? _dbContext.Disburments.Where(d => d.BorrowerCode == model.Borrower_Code).ToList() : _dbContext.Disburments.Where(d => d.BorrowerCode == model.Borrower_Code && (d.AdjustTowards == "prop" || d.AdjustTowards.Equals(Account))).ToList();
                    break;
                case StatementOfAccountMode.Interest:
                    InterestRateChangeEntries = _dbContext.InterestRateChangeEntries.Where(f => f.BorrowerCode == model.Borrower_Code).ToList();
                    break;
            }
            if (forceUpdate)
            {
                StateHasChanged();
            }
        }

        /// <summary>
        /// Changes the mode of the statement of account
        /// </summary>
        /// <param name="model"></param>
        private void ChangeMode(StatementOfAccountMode mode)
        {
            if(mode != Mode)
            {
                ClearEntries();
                Mode = mode;
            }
        }

        /// <summary>
        /// Adds the entry
        /// </summary>
        private void Add()
        {
            _errors.Clear();
            if(NewEntry.Mode == StatementOfAccountMode.Expenses)
            {
                ExpenseEntry entry;
                //TODO: Where should the note field values go in the expense entry
                if (ShouldAddExpense)
                {
                    entry = new ExpenseEntry()
                    {
                        PaymentAmount = NewEntry.Amount,
                        Borrower_Code = Model.Borrower_Code,
                        TrustCode = Model.Trust_Code,
                        Trust_Name = Model.Trust_Name,
                        PaymentDate = DateTime.Now,
                        AdjustTowards = NewEntry.AdjustTowards
                    };
                    _dbContext.Expenses.Add(entry);
                }
                else
                {
                    entry = _dbContext.Expenses.Where(f => f.Id == NewEntry.Id).First();
                    entry.PaymentAmount = NewEntry.Amount;
                    entry.AdjustTowards = NewEntry.AdjustTowards;
                    _dbContext.Expenses.Update(entry);
                }
                Save();

                if (!ShouldAddExpense)
                {
                    List<ExpenseAdjustmentModel> expenseAdjustments = _dbContext.ExpenseAdjustments.Where(f => f.ExpenseId == entry.Id).ToList();
                    _dbContext.ExpenseAdjustments.RemoveRange(expenseAdjustments);
                    _dbContext.SaveChanges();
                }

                if (NewEntry.AdjustTowards == "prop")
                {
                    double totalPos = DebtProfiles.Sum(f => f.POS);
                    DebtProfiles.ForEach(f =>
                    {
                        double proportion = f.POS / totalPos;
                        double amount = proportion * NewEntry.Amount;
                        _dbContext.ExpenseAdjustments.Add(new ExpenseAdjustmentModel()
                        {
                            AccountNumber = f.AccountNumber,
                            ExpenseId = entry.Id,
                            Amount = amount,
                            BorrowerCode = Model.Borrower_Code,
                        });
                    });
                }
                else
                {
                    _dbContext.ExpenseAdjustments.Add(new ExpenseAdjustmentModel()
                    {
                        AccountNumber = int.Parse(NewEntry.AdjustTowards),
                        Amount = NewEntry.Amount,
                        ExpenseId = entry.Id,
                        BorrowerCode = Model.Borrower_Code
                    });
                }

            }
            else if(NewEntry.Mode == StatementOfAccountMode.Collections)
            {
                int collectionId = 1;
                CollectionEntry collectionEntry;
                if (ShouldAddCollection)
                {
                    CollectionEntry? firstEntry = _dbContext.Collections.OrderByDescending(f => f.CollectionId).FirstOrDefault();

                    if (firstEntry != null && firstEntry.CollectionId.HasValue)
                        collectionId = firstEntry.CollectionId.Value + 1;

                    //Getting the borrower with the specified code
                    Account acc = _dbContext.Accounts.Where(f => f.BorrowerCode == Model.Borrower_Code).First();

                    collectionEntry = new CollectionEntry()
                    {
                        Adjustment = NewEntry.AdjustTowards,
                        AdjustToward = "----",
                        TrustCode = Model.Trust_Code,
                        Trust_Name = Model.Trust_Name,
                        BorrowerName = acc.Company,
                        Borrower = Model.Borrower_Code,
                        CollectionId = collectionId,
                        CreditAmount = NewEntry.Amount,
                        CreditDate = NewEntry.Date,
                        Source = "--Source--",
                        TypeOfRecovery = "--Recovery--"
                    };

                    _dbContext.Collections.Add(collectionEntry);
                }
                else
                {
                    collectionEntry = _dbContext.Collections.Where(f => f.Id == NewEntry.Id).First();
                    collectionId = collectionEntry.CollectionId.Value;
                    collectionEntry.Adjustment = NewEntry.AdjustTowards;
                    collectionEntry.CreditAmount = NewEntry.Amount;
                    collectionEntry.CreditDate = NewEntry.Date;
                    _dbContext.Collections.Update(collectionEntry);
                }

                Save();

                if (!ShouldAddCollection)
                {
                    List<CollectionAdjustmentModel> collectionAdjustments = _dbContext.CollectionAdjustments.Where(f => f.CombinedCollectionId == collectionId).ToList();
                    _dbContext.CollectionAdjustments.RemoveRange(collectionAdjustments);
                    _dbContext.SaveChanges();
                }

                //Setting up Adjustment Data
                if (NewEntry.AdjustTowards == "prop")
                {
                    double totalPos = DebtProfiles.Sum(f => f.POS);
                    DebtProfiles.ForEach(f =>
                    {
                        double proportion = f.POS / totalPos;
                        double amount = proportion * NewEntry.Amount;
                        _dbContext.CollectionAdjustments.Add(new CollectionAdjustmentModel()
                        {
                            AccountNumber = f.AccountNumber,
                            Amount = amount,
                            BorrowerCode = collectionEntry.Borrower,
                            CollectionId = collectionEntry.Id,
                            CombinedCollectionId = collectionEntry.CollectionId,
                            TrustCode = collectionEntry.TrustCode,
                        });
                    });
                }
                else
                {
                    _dbContext.CollectionAdjustments.Add(new CollectionAdjustmentModel()
                    {
                        AccountNumber = int.Parse(NewEntry.AdjustTowards),
                        Amount = collectionEntry.CreditAmount,
                        BorrowerCode = collectionEntry.Borrower,
                        CollectionId = collectionEntry.Id,
                        CombinedCollectionId = collectionEntry.CollectionId,
                        TrustCode = collectionEntry.TrustCode,
                    });
                }
            }
            else if(NewEntry.Mode == StatementOfAccountMode.Disbursement)
            {
                DisburmentModel disburment;

                if (ShouldAddDisbursment)
                {
                    disburment = new DisburmentModel()
                    {
                        BorrowerCode = Model.Borrower_Code,
                        Date = NewEntry.Date,
                        Note = NewEntry.Note,
                        Amount = NewEntry.Amount,
                        AdjustTowards = NewEntry.AdjustTowards
                    };
                    _dbContext.Disburments.Add(disburment);
                }
                else
                {
                    disburment = _dbContext.Disburments.Where(f => f.Id == NewEntry.Id).First();
                    disburment.Date = NewEntry.Date;
                    disburment.Amount = NewEntry.Amount;
                    disburment.AdjustTowards = NewEntry.AdjustTowards;
                    _dbContext.Disburments.Update(disburment);
                }

                Save();

                if (!ShouldAddDisbursment)
                {
                    List<DisbursmentAdjustmentModel> disbursmentAdjustments = _dbContext.DisbursmentAdjustments.Where(f => f.DisbursmentId == disburment.Id).ToList();
                    _dbContext.DisbursmentAdjustments.RemoveRange(disbursmentAdjustments);
                    _dbContext.SaveChanges();
                }

                //Setting up Adjustment Data
                if (NewEntry.AdjustTowards == "prop")
                {
                    double totalPos = DebtProfiles.Sum(f => f.POS);
                    DebtProfiles.ForEach(f =>
                    {
                        double proportion = f.POS / totalPos;
                        double amount = proportion * NewEntry.Amount;
                        _dbContext.DisbursmentAdjustments.Add(new DisbursmentAdjustmentModel()
                        {
                            AccountNumber = f.AccountNumber,
                            Amount = amount,
                            BorrowerCode = Model.Borrower_Code,
                            DisbursmentId = disburment.Id
                        });
                    });
                }
                else
                {
                    _dbContext.DisbursmentAdjustments.Add(new DisbursmentAdjustmentModel()
                    {
                        AccountNumber = int.Parse(NewEntry.AdjustTowards),
                        Amount = NewEntry.Amount,
                        BorrowerCode = Model.Borrower_Code,
                        DisbursmentId= disburment.Id
                    });
                }
            }
            else if (NewEntry.Mode == StatementOfAccountMode.Interest)
            {
                if (ShouldAddInterestRate)
                {
                    InterestRateChangeModel interestRate = new InterestRateChangeModel()
                    {
                        BorrowerCode = Model.Borrower_Code,
                        Date = NewEntry.Date,
                        Note = NewEntry.Note,
                        RevisedInterestRate = NewEntry.ChangeInInterestRate,
                    };
                    _dbContext.InterestRateChangeEntries.Add(interestRate);
                }
                else
                {
                    InterestRateChangeModel interestRate = _dbContext.InterestRateChangeEntries.Where(f => f.Id == NewEntry.Id).First();
                    interestRate.Date = NewEntry.Date;
                    interestRate.RevisedInterestRate = NewEntry.ChangeInInterestRate;
                    interestRate.Note = NewEntry.Note;
                    _dbContext.InterestRateChangeEntries.Update(interestRate);
                }
            }

            Save();

            UpdateEntries(Model, NewEntry.Mode, true);
            ClearSubEntry();
        }

        private void Save()
        {
            //Saving the database and in case of errors showing the errors to the user on the form
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {
                if (exc.InnerException != null)
                    _errors.Add(exc.InnerException.Message);
                else
                    _errors.Add(exc.Message);
            }
        }

        private void ShowDeleteConfirmationModal()
        {
            DeleteConfirmationModalShown = true;
        }

        private void CancelDelete()
        {
            DeleteConfirmationModalShown = false;
        }

        private void Remove()
        {
            DeleteConfirmationModalShown = false;
            if(Mode == StatementOfAccountMode.Collections && SelectedCollectionEntry != null) 
            {
                List<CollectionAdjustmentModel> collectionAdjustments = _dbContext.CollectionAdjustments.Where(f => f.CombinedCollectionId == SelectedCollectionEntry.CollectionId).ToList();
                _dbContext.CollectionAdjustments.RemoveRange(collectionAdjustments);
                _dbContext.SaveChanges();
                _dbContext.Collections.Remove(SelectedCollectionEntry);
            }
            else if (Mode == StatementOfAccountMode.Expenses && SelectedExpenseEntry != null)
            {
                List<ExpenseAdjustmentModel> expenseAdjustments = _dbContext.ExpenseAdjustments.Where(f => f.ExpenseId == SelectedExpenseEntry.Id).ToList();
                _dbContext.ExpenseAdjustments.RemoveRange(expenseAdjustments);
                _dbContext.SaveChanges();
                _dbContext.Expenses.Remove(SelectedExpenseEntry);
            }
            else if(Mode == StatementOfAccountMode.Interest && SelectedInterestRateEntry != null)
            {
                _dbContext.InterestRateChangeEntries.Remove(SelectedInterestRateEntry);
            }
            else if(Mode == StatementOfAccountMode.Disbursement && SelectedDisbursementEntry != null)
            {
                List<DisbursmentAdjustmentModel> disbursmentAdjustments = _dbContext.DisbursmentAdjustments.Where(f => f.DisbursmentId == SelectedDisbursementEntry.Id).ToList();
                _dbContext.DisbursmentAdjustments.RemoveRange(disbursmentAdjustments);
                _dbContext.SaveChanges();
                _dbContext.Disburments.Remove(SelectedDisbursementEntry);
            }
            _dbContext.SaveChanges();
            UpdateEntries(Model, Mode, true);
            ClearSubEntry();
        }

        private void ClearEntries()
        {
            SelectedExpenseEntry = null;
            SelectedCollectionEntry = null;
            SelectedDisbursementEntry = null;
            SelectedInterestRateEntry = null;
        }

        private void Clear()
        {
            NewEntry = new StatementOfAccountDataViewModel();
        }

        /// <summary>
        /// Loads data for pdf and excel
        /// </summary>
        private void LoadDataToPrint()
        {
            List<ExpenseEntry> expenses = _dbContext.Expenses.Where(f => f.Borrower_Code == Model.Borrower_Code).ToList();
            List<CollectionEntry> collections = _dbContext.Collections.Where(f => f.Borrower == Model.Borrower_Code).ToList();
            List<DisburmentModel> disburments = _dbContext.Disburments.Where(f => f.BorrowerCode == Model.Borrower_Code).ToList();
            List<InterestRateChangeModel> interestRateChangeModels = _dbContext.InterestRateChangeEntries.Where(f => f.BorrowerCode == Model.Borrower_Code).ToList();
            //The list containing data from all the above specified lists
            List<StatementOfAccountDataViewModel> items = new List<StatementOfAccountDataViewModel>();
            expenses.ForEach(e =>items.Add(StatementOfAccountDataViewModel.FromExpense(e)));
            collections.ForEach(e =>items.Add(StatementOfAccountDataViewModel.FromCollection(e)));
            disburments.ForEach(e =>items.Add(StatementOfAccountDataViewModel.FromDisbursment(e)));
            interestRateChangeModels.ForEach(e =>items.Add(StatementOfAccountDataViewModel.FromInterestRate(e)));
            items = items.OrderByDescending(e => e.Date).ToList();
            //<------------- All the data for the selected borrower is in the above items list sorted in descending order ---------------->
        }

        /// <summary>
        /// Loads data for pdf and excel
        /// </summary>
        private void LoadSummary()
        {
            List<ExpenseEntry> expenses = _dbContext.Expenses.ToList();
            List<CollectionEntry> collections = _dbContext.Collections.ToList();
            List<DisburmentModel> disburments = _dbContext.Disburments.ToList();
            List<InterestRateChangeModel> interestRateChangeModels = _dbContext.InterestRateChangeEntries.ToList();
            //The list containing data from all the above specified lists
            List<StatementOfAccountDataViewModel> items = new List<StatementOfAccountDataViewModel>();
            expenses.ForEach(e => items.Add(StatementOfAccountDataViewModel.FromExpense(e)));
            collections.ForEach(e => items.Add(StatementOfAccountDataViewModel.FromCollection(e)));
            disburments.ForEach(e => items.Add(StatementOfAccountDataViewModel.FromDisbursment(e)));
            interestRateChangeModels.ForEach(e => items.Add(StatementOfAccountDataViewModel.FromInterestRate(e)));
            items = items.OrderByDescending(e => e.Date).ToList();
            //<------------- All the data for all the borrowers is in the above items list sorted in descending order ---------------->
        }

        /// <summary>
        /// Clears sub entry data
        /// </summary>
        private void ClearSubEntry()
        {
            Clear();
            SelectedCollectionEntry = null;
            SelectedDisbursementEntry = null;
            SelectedExpenseEntry = null;
            SelectedInterestRateEntry = null;
            ShouldAddCollection = ShouldAddDisbursment = ShouldAddExpense = ShouldAddInterestRate = true;
            StateHasChanged();
        }

        #endregion

        #region Public Methods

        public int GetId(StatementOfAccountViewModel t) => t.Borrower_Code;

        #endregion
    }
}
