using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Base
{
	public partial class BaseAddPage<T> : ComponentBase
		where T: BaseModel, new()
	{

		#region Protected Properties

		protected bool IsAddMode { get; set; } = true;

		protected T NewEntry { get; set; } = new T();

		protected List<string> _errors = new List<string>();

		#endregion

		#region Injected Members

		[Inject]
		protected ApplicationDbContext _dbContext { get; set; }

		[Inject]
		protected ProtectedSessionStorage _storage { get; set; }

		[Inject]
		protected NavigationManager _navigationManager { get; set; }

		#endregion

		#region Protected Members

		protected string PageModeKey { get; set; } = string.Empty;

		protected string IdKey { get; set; } = string.Empty;

		protected string ReturnUrl { get; set; } = string.Empty;

		protected Func<List<T>>? Records { get; set; } = null;

		protected Func<T, bool, bool>? Save { get; set; } = null;

		#endregion

		#region Overriden Methods

		protected override async void OnInitialized()
        {
			Setup();
            ProtectedBrowserStorageResult<string> pageModeResult = await _storage.GetAsync<string>(PageModeKey);
            if (pageModeResult.Success)
            {
                if (pageModeResult.Value == "edit")
                {
                    IsAddMode = false;
                    ProtectedBrowserStorageResult<int> idRes = await _storage.GetAsync<int>(IdKey);
                    if (idRes.Success && Records != null)
                    {
                        NewEntry = Records().Where(r => r.Id == idRes.Value).First();
                        StateHasChanged();
                    }
                }
            }
        }

		#endregion

		#region Protected Methods

		protected virtual void Setup(){}

		protected void Add()
		{
			if (Save != null)
			{
				if (!Save(NewEntry, IsAddMode))
					return;
				_dbContext.SaveChanges();
				_navigationManager.NavigateTo(ReturnUrl);
            }
		}

        protected void Cancel()
        {
			_navigationManager.NavigateTo(ReturnUrl);
        }

		protected void Clear()
		{
			NewEntry = new T();
		}

        #endregion

    }
}
