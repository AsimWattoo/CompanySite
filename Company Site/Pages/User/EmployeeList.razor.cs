using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

using data = Company_Site.Data;

namespace Company_Site.Pages.User
{
    public partial class EmployeeList
    {
        #region Private Members

        private const string BASE_URL = "/img/Profile/";

        /// <summary>
        /// The list of borrowers
        /// </summary>
        private List<Account> Borrowers = new List<Account>();

        /// <summary>
        /// The list of borrowers for the employee
        /// </summary>
        private List<int> selectedBorrowers = new List<int>();

        /// <summary>
        /// The list of pages for access
        /// </summary>
        private List<PageAccess> PageAccesses = new List<PageAccess>()
        {
            new PageAccess(){ Page = UserPages.Finance },
            new PageAccess(){ Page = UserPages.Account },
            new PageAccess(){ Page = UserPages.TrustData },
            new PageAccess(){ Page = UserPages.Legal },
        };

        /// <summary>
        /// The list of selected pages
        /// </summary>
        private List<UserPages> selectedPages = new List<UserPages>();

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

        [Inject]
        private UserManager<data.User> _userManager { get; set; }

        [Inject]
        private IPasswordHasher<data.User> _passwordHasher { get; set; }

        #endregion

        #region Overriden Methods

        protected override async void OnInitialized()
        {
            Setup();
            Enteries = _dbSet.ToList();
            Borrowers = _dbContext.Accounts.ToList();
        }

        #endregion

        #region Protected Methods

        protected virtual async void Save()
        {
            _errors.Clear();
            if (ShouldAdd)
            {
                if(Password == null)
                {
                    _errors.Add("Please enter a valid password");
                    return;
                }
                NewEntry.UserName = NewEntry.FirstName + NewEntry.LastName;
                NewEntry.Id = Guid.NewGuid().ToString();
                NewEntry.Access = "";
                foreach (int access in selectedBorrowers)
                    NewEntry.Access += $"{access}:";
                NewEntry.Access = NewEntry.Access.Trim(':');
                NewEntry.PageAccess = "";
                foreach (UserPages page in selectedPages)
                    NewEntry.PageAccess += $"{page}:";
                NewEntry.PageAccess = NewEntry.PageAccess.Trim(':');
                IdentityResult res = await _userManager.CreateAsync(NewEntry, Password);
                if (!res.Succeeded)
                {
                    foreach(IdentityError error in res.Errors) 
                    {
                        _errors.Add(error.Description);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    NewEntry.PasswordHash = _passwordHasher.HashPassword(NewEntry, Password);
                }
                NewEntry.UserName = NewEntry.FirstName + NewEntry.LastName;
                NewEntry.Access = "";
                foreach (int access in selectedBorrowers)
                    NewEntry.Access += $"{access}:";
                NewEntry.Access = NewEntry.Access.Trim(':');
                NewEntry.PageAccess = "";
                foreach (UserPages page in selectedPages)
                    NewEntry.PageAccess += $"{page}:";
                NewEntry.PageAccess = NewEntry.PageAccess.Trim(':');
                _dbSet.Update(NewEntry);
            }
            _dbContext.SaveChanges();
            Setup();
            ShouldAdd = true;
            RenderFile("");
            StateHasChanged();
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }

        public void EditRecord(string id)
        {
            ShouldAdd = false;
            NewEntry = _dbSet.Where(t => t.Id.Equals(id)).First();
            selectedBorrowers = new List<int>();
            if(!string.IsNullOrEmpty(NewEntry.Access))
            {
                string[] parts = NewEntry.Access.Split(':');
                foreach (string part in parts)
                {
                    selectedBorrowers.Add(int.Parse(part));
                }
            }
            selectedPages = new List<UserPages>();
            if (!string.IsNullOrEmpty(NewEntry.PageAccess))
            {
                string[] parts = NewEntry.PageAccess.Split(':');
                foreach (string part in parts)
                {
                    UserPages page;
                    if (System.Enum.TryParse(part, out page))
                    {
                        selectedPages.Add(page);
                    }
                }
            }
            //Rendering Existing Image
            if (NewEntry.ProfileImage != null)
            {
                FileInfo info = new FileInfo(NewEntry.ProfileImage);
                RenderFile($"{BASE_URL}{info.Name}");
            }
            StateHasChanged();
        }

        protected void Clear()
        {
            Setup();
            ShouldAdd = true;
            StateHasChanged();
            RenderFile("");
        }

        public void DeleteRecord(string id)
        {
            _dbSet.Remove(_dbSet.Where(f => f.Id == id).First());
            _dbContext.SaveChanges();
            Setup();
            StateHasChanged();
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }

        protected void Setup()
        {
            _dbSet = _dbContext.Users;
            Password = null;
            Enteries = _dbSet.ToList();
            selectedBorrowers.Clear();
            selectedPages.Clear();
            NewEntry = new data.User();
            NewEntry.Id = Guid.NewGuid().ToString();
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

        private void AccessListChanged(object accessList)
        {
            if(accessList is IEnumerable<Int32> list)
            {
                selectedBorrowers = list.ToList();
            }
        }

        private void PageAccessChanged(object accessList)
        {
            if (accessList is IEnumerable<UserPages> list)
            {
                selectedPages = list.ToList();
            }
        }

        #endregion

    }
}
