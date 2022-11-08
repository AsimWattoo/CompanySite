using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class ExpenseManager : ComponentBase, ITable<ExpenseEntry, int>
    {
        #region Private Members

        /// <summary>
        /// The expenses to be shown
        /// </summary>
        public List<ExpenseEntry> Enteries { get; set; } = new List<ExpenseEntry>();

        public Dictionary<string, Func<ExpenseEntry, string>> Headers { get; set; } = new Dictionary<string, Func<ExpenseEntry, string>>()
        {
            ["Trust Code"] = (ExpenseEntry e) => e.TrustCode,
            ["Borrower Code"] = (ExpenseEntry e) => e.Borrower_Code,
            ["Trust Name"] = (ExpenseEntry e) => e.Trust_Name,
            ["Vendor"] = (ExpenseEntry e) => e.Vendor.VendorName,
            ["Service"] = (ExpenseEntry e) => e.Service,
            ["Amount"] = (ExpenseEntry e) => e.BillAmount.ToString(),
            ["Payment Date"] = (ExpenseEntry e) => e.PaymentDate.ToString("dd/MM/yyyy"),
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
        public int GetId(ExpenseEntry e) => e.Id;

        /// <summary>
        /// Deletes the expense entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ExpenseEntry> DeleteRecord(int id)
        {
            Enteries.Remove(Enteries.Where(f => f.Id == id).First());
            return Enteries;
        }

        /// <summary>
        /// Edits the record
        /// </summary>
        /// <param name="id"></param>
        public void EditRecord(int id)
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
        public List<ExpenseEntry> Search(List<ExpenseEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Borrower_Code.Equals(text) || e.Trust_Name.Equals(text) || e.Service.Equals(text)).ToList();
        }

        /// <summary>
        /// Specifies each table row
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public List<string> GetTableRows (ExpenseEntry ex)
        {
            return new List<string>()
            {
                ex.TrustCode,
                ex.Borrower_Code,
                ex.Trust_Name,
                ex.Vendor.Id.ToString(),
                ex.Service,
                ex.BillAmount.ToString(),
                ex.PaymentDate.ToString("dd/MM/yyyy")
            };
        }

        #endregion
    }
}
