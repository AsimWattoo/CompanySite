using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class ExpenseManager : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The expenses to be shown
        /// </summary>
        private List<ExpenseEntry> Enteries { get; set; } = new List<ExpenseEntry>();

        private Dictionary<string, Func<ExpenseEntry, dynamic>> Headers { get; set; } = new Dictionary<string, Func<ExpenseEntry, dynamic>>()
        {
            ["Id"] = (ExpenseEntry e) => e.Id,
            ["Trust Code"] = (ExpenseEntry e) => e.TrustCode,
            ["Borrower Code"] = (ExpenseEntry e) => e.Borrower_Code,
            ["Trust Name"] = (ExpenseEntry e) => e.Trust_Name,
            ["Vendor"] = (ExpenseEntry e) => e.Vendor,
            ["Service"] = (ExpenseEntry e) => e.Service,
            ["Amount"] = (ExpenseEntry e) => e.BillAmount,
            ["Payment Date"] = (ExpenseEntry e) => e.PaymentDate,
        };

        #endregion

        #region Injected Members

        [Inject]
        private ProtectedSessionStorage _sessionStorage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates to the add page
        /// </summary>
        private void GoToAddPage()
        {
            _sessionStorage.SetAsync("ExpensePageMode", "add");
            _navigationManager.NavigateTo("/finance/expense/modify");
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private int GetExpenseId(ExpenseEntry e) => e.Id;

        /// <summary>
        /// Deletes the expense entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ExpenseEntry> DeleteRecord(int id)
        {
            Enteries.Remove(Enteries.Where(f => f.Id == id).First());
            return Enteries;
        }

        /// <summary>
        /// Edits the record
        /// </summary>
        /// <param name="id"></param>
        private void EditRecord(int id)
        {
            _sessionStorage.SetAsync("ExpensePageMode", "edit");
            _sessionStorage.SetAsync("ExpenseId", id);
            _navigationManager.NavigateTo("/finance/expense/modify");
        }

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<ExpenseEntry> Search(List<ExpenseEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Borrower_Code.Equals(text) || e.Trust_Name.Equals(text) || e.Service.Equals(text)).ToList();
        }

        /// <summary>
        /// Specifies each table row
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private List<string> GetTableRows (ExpenseEntry ex)
        {
            return new List<string>()
            {
                ex.Id.ToString(),
                ex.TrustCode,
                ex.Borrower_Code,
                ex.Trust_Name,
                ex.Vendor.Id.ToString(),
                ex.Service,
                ex.BillAmount.ToString(),
                ex.PaymentDate.ToShortDateString()
            };
        }

        #endregion
    }
}
