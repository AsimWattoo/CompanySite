using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

using data = Company_Site.Data;

namespace Company_Site.Pages.User
{
    public partial class EmployeeList
    {
        #region Private Members

        private const string BASE_URL = "/img/Profile/";

        #endregion

        #region Public Members

        [CascadingParameter(Name = "UserId")]
        public string UserId { get; set; }

        [CascadingParameter(Name = "IsAdmin")]
        public bool IsAdmin { get; set; }

        #endregion

        #region Protected Properties

        public List<data.User> Enteries { get; set; } = new List<data.User>();

        protected data.User NewEntry { get; set; } = new data.User();

        protected List<string> _errors = new List<string>();

        protected DbSet<data.User> _dbSet { get; set; }

        protected bool ShouldAdd { get; set; } = true;

        private IBrowserFile File { get; set; }

        private string Password { get; set; }

        #endregion

        #region Injected Members

        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        protected ApplicationDbContext _dbContext { get; set; }

        [Inject]
        protected ProtectedSessionStorage _storage { get; set; }

        [Inject]
        protected NavigationManager _navigationManager { get; set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        #endregion

        #region Overriden Methods

        protected override async void OnInitialized()
        {
            Setup();
            Enteries = _dbSet.ToList();
        }

        #endregion

        #region Protected Methods

        protected virtual void Save()
        {
            if (ShouldAdd)
                _dbSet.Add(NewEntry);
            else
                _dbSet.Update(NewEntry);
            _dbContext.SaveChanges();
            NewEntry = new data.User();
            Enteries = _dbSet.ToList();
            ShouldAdd = true;
            RenderFile("");
            StateHasChanged();
        }

        public void EditRecord(string id)
        {
            ShouldAdd = false;
            NewEntry = _dbSet.Where(t => t.Id.Equals(id)).First();
            //Rendering Existing Image
            if(NewEntry.ProfileImage != null)
            {
                FileInfo info = new FileInfo(NewEntry.ProfileImage);
                RenderFile($"{BASE_URL}{info.Name}");
            }
            StateHasChanged();
        }

        protected void Clear()
        {
            NewEntry = new data.User();
            ShouldAdd = true;
            StateHasChanged();
            RenderFile("");
        }

        public void DeleteRecord(string id)
        {
            _dbSet.Remove(_dbSet.Where(f => f.Id == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbSet.ToList();
            NewEntry = new data.User();
            StateHasChanged();
        }

        protected void Setup()
        {
            _dbSet = _dbContext.Users;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async Task ProfileImageChanged(InputFileChangeEventArgs e)
        {
            File = e.File;
            //Remove the previous image
            if(NewEntry.ProfileImage != null)
            {
                FileInfo info = new FileInfo(NewEntry.ProfileImage);
                info.Delete();
            }

            string fileName = File.Name.Replace(" ", "-");
            string imagePath = Path.Combine("img", "Profile", $"Profile-{UserId}-{fileName}");
            string url = $"{BASE_URL}Profile-{UserId}-{fileName}";
            string filePath = Path.Combine(WebHostEnvironment.WebRootPath, imagePath);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Create)))
            {
                await File.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            NewEntry.ProfileImage = filePath;
            RenderFile(url);
        }

        private async void RenderFile(string path)
        {
            await JS.InvokeVoidAsync("LoadImage", "#imageContainer", path);
        }

        #endregion

    }
}
