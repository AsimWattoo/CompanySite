using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Company_Site.Pages.User.Finance_Components
{
	public partial class CollectionEntryAdd : ComponentBase
	{

		#region Private Memebers

		/// <summary>
		/// Indicates whether the current mode is add mode or not
		/// </summary>
		private bool IsAddMode { get; set; } = true;

		/// <summary>
		/// The new entry
		/// </summary>
		private CollectionEntry NewEntry { get; set; } = new CollectionEntry();

		#endregion

		#region Injected Members

		[Inject]
		private ProtectedSessionStorage _sessionManager { get; set; }

		[Inject]
		private NavigationManager _navigationManager { get; set; }

		#endregion

		#region Overridden Methods

		/// <summary>
		/// Fires when the component is initialized
		/// </summary>
		protected override async Task OnInitializedAsync()
		{
			//Checking for the current page mode
			ProtectedBrowserStorageResult<string> res = await _sessionManager.GetAsync<string>("CollectionPageMode");
			if (res.Success)
			{
				if (res.Value == "edit")
					IsAddMode = false;
			}
		}

		/// <summary>
		/// Returns the user back to the page from where he/she got here
		/// </summary>
		private async void GoBack()
		{
			await _sessionManager.SetAsync("FinancePageMode", "collection");
			_navigationManager.NavigateTo("/finance");
		}

        /// <summary>
        /// Clears the values for the enteries
        /// </summary>
        private void Clear()
        {
            NewEntry = new CollectionEntry();
        }

        #endregion

    }
}
