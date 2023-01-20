using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Company_Site.Pages.Components
{
    public partial class ContactList<T> : ComponentBase
    {
        #region Parameters

        [Parameter]
        public List<T> Enteries { get; set; }

        [Parameter]
        public Func<T,string> GetContactName { get; set; }

        [Parameter]
        public Func<T, string> GetContactId { get; set; }

        [Parameter]
        public bool ShouldRenderFragment { get; set; } = false;

        /// <summary>
        /// Fires when a contact is deleted
        /// </summary>
        [Parameter]
        public Action<string> OnDelete { get; set; }

        /// <summary>
        /// Enables searching without pressing search button
        /// </summary>
        [Parameter]
        public bool AutoSearch { get; set; }

        /// <summary>
        /// Fires when a contact is clicked
        /// </summary>
        [Parameter]
        public Action<string> OnClick { get; set; }

        [Parameter]
        public string Title { get; set; } = "Contacts List";

        [Parameter]
        public bool ShowDeleteButton { get; set; } = true;

        [Parameter]
        public bool ShowDescription { get; set; } = false;

        [Parameter]
        public bool IsHeaderPrimary { get; set; } = false;

        [Parameter]
        public Func<T, string> GetDescription { get; set; }

        [Parameter]
        public Func<string, List<T>> OnSearch { get; set; }

        #endregion

        #region Private Members

        private string? _id = null;

        /// <summary>
        /// Indicates whether the delete confirmation is displayed or not
        /// </summary>
        private bool _isDeleteConfirmationShown = false;

        private string searchText = "";

        private List<T> _recordsToShow = new List<T>();

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the control is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            _recordsToShow = Enteries;
        }

        #endregion

        #region Private Methods

        private void OnDeleteRecord(string id)
        {
            _id = id;
            _isDeleteConfirmationShown = true;
        }

        private void CancelDelete()
        {
            _isDeleteConfirmationShown = false;
        }

        private void Delete()
        {
            _isDeleteConfirmationShown = false;
            OnDelete(_id);
        }

        /// <summary>
        /// Searches
        /// </summary>
        /// <param name="text"></param>
        private void Search(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _recordsToShow = Enteries;
                return;
            }
            if (OnSearch != null)
                _recordsToShow = OnSearch.Invoke(text);
        }

        /// <summary>
        /// Searches
        /// </summary>
        /// <param name="text"></param>
        private void AutoSearchText(ChangeEventArgs e)
        {
            string text = e.Value.ToString();
            if (string.IsNullOrEmpty(text))
            {
                _recordsToShow = Enteries;
                return;
            }
            if (OnSearch != null)
                _recordsToShow = OnSearch.Invoke(text);
        }

        #endregion
    }
}
