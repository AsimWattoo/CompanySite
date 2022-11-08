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

        #endregion

        #region Private Members

        private string? _id = null;

        #endregion

        #region Private Methods

        private void OnDeleteRecord(string id)
        {
            _id = id;
        }

        #endregion
    }
}
