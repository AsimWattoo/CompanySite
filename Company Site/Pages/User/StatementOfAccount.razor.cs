using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class StatementOfAccount : ComponentBase
    {

        #region Private Members

        /// <summary>
        /// The list of statements for an account
        /// </summary>
        private List<StatementOfAccountViewModel> Statements { get; set; } = new List<StatementOfAccountViewModel>();

        public Dictionary<string, Func<StatementOfAccountViewModel, string>> Headers { get; set; } = new Dictionary<string, Func<StatementOfAccountViewModel, string>>()
        {
            ["Sr No"] = p => p.SRNo.ToString(),
            ["Trust Code"] = p => p.Trust_Code,
            ["Borrower Code"] = p => p.Borrower_Code,
            ["Trust Name"] = p => p.Trust_Name,
            ["Account Number"] = p => p.Account_Number,
            ["Account Name"] = p => p.Account_Name,
            ["Bank Name"] = p => p.Bank_Name,
            ["Status"] = p => p.StatusAs,
            ["Facility"] = p => p.Facility,
            ["Date"] = p => p.SOADate.ToString("dd/MM/yyyy"),
            ["Opening Balance"] = p => p.Opening_Balance.ToString(),
            ["Interest"] = p => p.Interest.ToString(),
            ["Penal Interest (Rs.)"] = p => p.Penal_Interest.ToString(),
            ["Penal Percent (%)"] = p => p.Penal_Percent.ToString(),
            ["Frequency"] = p => p.Frequency,
            ["Calculation"] = p => p.Calculation,
            ["Note"] = p => p.Note,
            ["Closed Date"] = p => p.ClosedDate.ToString("dd/MM/yyyy"),
            ["Maker"] = p => p.Maker,
            ["Checker"] = p => p.Checker,
            ["Entry Time"] = p => p.EntryTime.ToShortTimeString(),
        };

        #endregion

        #region Public Methods

        public int GetId(StatementOfAccountViewModel t) => t.SRNo;

        #endregion

    }
}
