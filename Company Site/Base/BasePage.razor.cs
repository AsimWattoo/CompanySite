using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Base
{
	public partial class BasePage<T> : ComponentBase
		where T: BaseModel, new()
	{

        #region Protected Properties

        public List<T> Enteries { get; set; } = new List<T>();

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

		protected string AddPageUrl { get; set; } = string.Empty;

		#endregion

		#region Overriden Methods

		protected override async void OnInitialized()
        {
			Setup();
        }

		#endregion

		#region Protected Methods

		protected virtual void Setup(){}

        public void EditRecord(int id)
        {
            _storage.SetAsync(PageModeKey, "edit");
            _storage.SetAsync(IdKey, id);
            _navigationManager.NavigateTo(AddPageUrl);
        }

        public void GoToAddPage()
        {
            _storage.SetAsync(PageModeKey, "add");
            _navigationManager.NavigateTo(AddPageUrl);
        }

        #endregion

    }
}
