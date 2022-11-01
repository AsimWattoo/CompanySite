using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Turst_Components
{
	public partial class TrustMasterAdd : ComponentBase
	{

		#region Private Memebers

		/// <summary>
		/// Indicates whether the current mode is add mode or not
		/// </summary>
		private bool IsAddMode { get; set; } = true;

		/// <summary>
		/// The new entry
		/// </summary>
		private Trust NewEntry { get; set; } = new Trust();

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
			ProtectedBrowserStorageResult<string> res = await _sessionManager.GetAsync<string>("TrustPageMode");
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
			_navigationManager.NavigateTo("/trustmaster");
		}

		/// <summary>
		/// Clears the values for the enteries
		/// </summary>
		private void Clear()
		{
			NewEntry = new Trust();
		}

		#endregion

	}
}
