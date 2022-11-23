using Company_Site.Data;
using Company_Site.Enum;

namespace Company_Site.ViewModels
{
    public class StatementOfAccountDataViewModel
    {
        public StatementOfAccountMode Mode { get; set; } = StatementOfAccountMode.Expenses;

        public double Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsInterestRateChanged { get; set; }

        public double ChangeInInterestRate { get; set; }

        public string? Note { get; set; }

        public string? AdjustTowards { get; set; }

        public int BorrowerCode { get; set; }

        #region Public Methods

        public static StatementOfAccountDataViewModel FromExpense(ExpenseEntry e)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Amount = e.PaymentAmount;
            model.BorrowerCode = e.Borrower_Code;
            model.Date = e.PaymentDate;
            return model;
        }

        public static StatementOfAccountDataViewModel FromCollection(CollectionEntry e)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Amount = e.CreditAmount;
            model.Date = e.CreditDate;
            model.BorrowerCode = e.Borrower;
            model.AdjustTowards = e.AdjustToward;
            return model;
        }

        public static StatementOfAccountDataViewModel FromDisbursment(DisburmentModel d)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Amount = d.Amount;
            model.Date = d.Date;
            model.BorrowerCode = d.BorrowerCode;
            model.AdjustTowards = d.AdjustTowards;
            model.Note = d.Note;
            return model;
        }

        public static StatementOfAccountDataViewModel FromInterestRate(InterestRateChangeModel i)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.ChangeInInterestRate = i.RevisedInterestRate;
            model.Date = i.Date;
            model.BorrowerCode = i.BorrowerCode;
            model.Note = i.Note;
            return model;
        }

        #endregion

    }
}
