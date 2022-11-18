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

    }
}
