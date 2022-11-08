using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class CollectionManager : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The expenses to be shown
        /// </summary>
        private List<CollectionEntry> Enteries { get; set; } = new List<CollectionEntry>();

        private Dictionary<string, Func<CollectionEntry, string>> Headers { get; set; } = new Dictionary<string, Func<CollectionEntry, string>>()
        {
            ["Trust Code"] = (CollectionEntry e) => e.TrustCode,
            ["Borrower"] = (CollectionEntry e) => e.Borrower,
            ["Trust Name"] = (CollectionEntry e) => e.Trust_Name,
            ["Borrower Name"] = (CollectionEntry e) => e.BorrowerName,
            ["Amount"] = (CollectionEntry e) => e.CreditAmount.ToString(),
            ["Credit Date"] = (CollectionEntry e) => e.CreditDate.ToString("dd/MM/yyyy"),
            ["Source"] = (CollectionEntry e) => e.Source,
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
            _sessionStorage.SetAsync("CollectionPageMode", "add");
            _navigationManager.NavigateTo("/finance/collection/modify");
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private int GetCollectionId(CollectionEntry e) => e.Id;

        /// <summary>
        /// Deletes the expense entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<CollectionEntry> DeleteRecord(int id)
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
            _sessionStorage.SetAsync("CollectionPageMode", "edit");
            _sessionStorage.SetAsync("CollectionId", id);
            _navigationManager.NavigateTo("/finance/collection/modify");
        }

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<CollectionEntry> Search(List<CollectionEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Source.Equals(text) || e.Trust_Name.Equals(text) || e.Borrower.Equals(text) || e.BorrowerName.Equals(text)).ToList();
        }

        /// <summary>
        /// Specifies each table row
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private List<string> GetTableRows (CollectionEntry ex)
        {
            return new List<string>()
            {
                ex.Id.ToString(),
                ex.TrustCode,
                ex.Borrower,
                ex.Trust_Name,
                ex.BorrowerName,
                ex.CreditAmount.ToString(),
                ex.CreditDate.ToString("dd/mm/yyyy"),
                ex.Source
            };
        }

        #endregion
    }
}
