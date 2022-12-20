using Microsoft.AspNetCore.Components;

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

        /// <summary>
        /// Fires when a contact is deleted
        /// </summary>
        [Parameter]
        public Action<string> OnDelete { get; set; }

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

        #endregion

        #region Private Members

        private string? _id = null;

        /// <summary>
        /// Indicates whether the delete confirmation is displayed or not
        /// </summary>
        private bool _isDeleteConfirmationShown = false;

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

        #endregion
    }
}
