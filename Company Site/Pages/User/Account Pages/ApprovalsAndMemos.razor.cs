using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Enum;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class ApprovalsAndMemos : BaseAddPage<ApprovalsAndMemoModel, int>
    {
        #region Public Properties

        public IBrowserFile NewFile { get; set; }

        public Dictionary<string, Func<Memo, string>> Headers { get; set; } = new Dictionary<string, Func<Memo, string>>()
        {
            ["Date"] = m => "",
            ["Committe"] = m => "",
            ["Description"] = m => "",
        };

        /// <summary>
        /// The list of memos to display
        /// </summary>
        public List<Memo> memos = new List<Memo>();

        #endregion

        #region Injected Members

        [Inject]
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        [Inject]
        public NavigationManager navManager { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the control is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            memos = _dbContext.Memos.Where(f => f.BorrowerCode == _applicationState.BorrowerCode && f.MemoStatus == MemoStatus.Approved).ToList();
            base.OnInitialized();
        }

        protected override void Setup()
        {
            _dbSet = _dbContext.ApprovalsAndMemoModels;
        }

		protected override void LoadData()
		{
            _errors.Clear();
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Select an account from the account management page");
                return;
            }
            //Getting the entry from the database
            ApprovalsAndMemoModel? model = _dbContext.ApprovalsAndMemoModels.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            //If entry is null that means that no entry exists for the currently selected account
            if(model == null)
            {
                NewEntry = new ApprovalsAndMemoModel();
                ShouldAdd = true;
            }
            else
            {
                ShouldAdd = false;
                NewEntry = model;
            }
            StateHasChanged();
		}

		#endregion

		#region Public Methods

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async void SupportingFileChanged(InputFileChangeEventArgs e)
        {
            _errors.Clear();
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page");
                return;
            }

            NewFile = e.File;

            string fileName = NewFile.Name.Replace(" ", "-");
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Approvals_Docs");

            //Checking if previous file exists
            if (NewEntry.FilePath != null)
            {
                //Deleting if a new file has been added
                System.IO.File.Delete(Path.Combine(folderPath, NewEntry.FilePath));
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            fileName = $"{_applicationState.BorrowerCode}-{fileName}";
            string path = Path.Combine(folderPath, fileName);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
            {
                await NewFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            NewEntry.FilePath = fileName;
        }

        /// <summary>
        /// Saves the record for approval and memo
        /// </summary>
        /// <returns></returns>
		protected override bool SaveSetup()
		{
            _errors.Clear();
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page");
                return false;
            }
            
            if (ShouldAdd)
            {
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
                _dbContext.ApprovalsAndMemoModels.Add(NewEntry);
            }
            else
            {
                _dbContext.ApprovalsAndMemoModels.Update(NewEntry);
                ShouldAdd = false;
            }
            return true;
		}

		protected override void SaveResetup(){}

		protected override void OnClear()
        {
            _errors.Clear();
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page");
            }
            ApprovalsAndMemoModel? model = _dbContext.ApprovalsAndMemoModels.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            if (model == null)
                NewEntry = new ApprovalsAndMemoModel();
            else
                NewEntry = model;
            StateHasChanged();
        }

        #endregion

        #region Private Members

        private string GetId(Memo m) => m.MemoNumber;

        /// <summary>
        /// Returns values for headers
        /// </summary>
        /// <param name="header"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetValue(string header, Memo item)
        {
            switch (header)
            {
                case "Date":
                    return item.Date.ToString("dd-MMM-yyyy");
                case "Committe":
                    List<int> references = _dbContext.MemoReferences.Where(f => f.Id == item.ToId && f.MemoId == item.MemoNumber).Select(f => f.UserId).ToList();
                    string users = "";
                    references.ForEach(reference =>
                    {
                        Company_Site.Data.User user = _dbContext.Users.Where(f => f.Id == reference).First();
                        users += $"{user.FirstName} {user.LastName} - ";
                    });
                    return users;
                case "Description":
                    return item.Subject;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Navigates to the new
        /// </summary>
        /// <param name="memoNumber">The id of the memo to view</param>
        private void ViewMemo(string memoNumber)
        {
            navManager.NavigateTo(QueryHelpers.AddQueryString("/memoview", "memoNumber", memoNumber));
        }

        /// <summary>
        /// Searches memo based on string
        /// </summary>
        /// <returns>The list of searched memos</returns>
        private List<Memo> SearchMemo(List<Memo> memos, string text)
        {
            return memos.Where(f => f.Subject.Contains(text)).ToList();
        }

        private void ApplyDateFilter(DateTime startDate, DateTime endDate)
        {
            memos = _dbContext.Memos.Where(f => f.BorrowerCode == _applicationState.BorrowerCode && f.MemoStatus == MemoStatus.Approved && f.Date >= startDate && f.Date <= endDate).ToList();
            StateHasChanged();
        }

        private void ClearDateFilter()
        {
            memos = _dbContext.Memos.Where(f => f.BorrowerCode == _applicationState.BorrowerCode && f.MemoStatus == MemoStatus.Approved).ToList();
            StateHasChanged();
        }

        #endregion
    }
}
