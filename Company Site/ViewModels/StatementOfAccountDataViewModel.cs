using Company_Site.Data;
using Company_Site.Enum;

using Microsoft.AspNetCore.Routing.Constraints;

namespace Company_Site.ViewModels
{
    public class StatementOfAccountDataViewModel
    {
        public StatementOfAccountMode Mode { get; set; } = StatementOfAccountMode.Expenses;

        public int Id { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsInterestRateChanged { get; set; }

        public double BaseInterestRate { get; set; }

        public double Spread { get; set; }

        public double RevisedInterestRate => BaseInterestRate + Spread;

        public string? Note { get; set; }

        public string? AdjustTowards { get; set; }

        public int BorrowerCode { get; set; }

        #region Public Methods

        public static StatementOfAccountDataViewModel FromExpense(ExpenseEntry e)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Mode = StatementOfAccountMode.Expenses;
            model.Amount = e.PaymentAmount;
            model.BorrowerCode = e.Borrower_Code;
            model.Date = e.PaymentDate;
            model.Id = e.Id;
            model.AdjustTowards = e.AdjustTowards;
            return model;
        }

        public static StatementOfAccountDataViewModel FromCollection(CollectionEntry e)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Amount = e.CreditAmount;
            model.Mode = StatementOfAccountMode.Collections;
            model.Date = e.CreditDate;
            model.BorrowerCode = e.Borrower;
            model.AdjustTowards = e.Adjustment;
            model.Id = e.Id;
            return model;
        }

        public static StatementOfAccountDataViewModel FromDisbursment(DisburmentModel d)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.Amount = d.Amount;
            model.Mode = StatementOfAccountMode.Disbursement;
            model.Date = d.Date;
            model.BorrowerCode = d.BorrowerCode;
            model.AdjustTowards = d.AdjustTowards;
            model.Note = d.Note;
            model.Id = d.Id;
            return model;
        }

        public static StatementOfAccountDataViewModel FromInterestRate(InterestRateChangeModel i)
        {
            StatementOfAccountDataViewModel model = new StatementOfAccountDataViewModel();
            model.BaseInterestRate = i.BaseInterestRate;
            model.Spread = i.Spread;
            model.Date = i.Date;
            model.Mode = StatementOfAccountMode.Interest;
            model.BorrowerCode = i.BorrowerCode;
            model.Note = i.Note;
            model.Id = i.Id;
            return model;
        }

        #endregion

    }
}
