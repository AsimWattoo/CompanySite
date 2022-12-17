using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System.Linq.Expressions;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class NoticeAutomation : BaseAddPage<NoticeAutomationModel, int>
    {
        #region Private Members

        /// <summary>
        /// The list of saved drafts
        /// </summary>
        private List<DraftModel> SavedDrafts { get; set; } = new List<DraftModel>();

        private DraftModel NewDraft { get; set; } = new DraftModel();

        private bool ShouldAddDraft { get; set; } = true;

        /// <summary>
        /// The currently uploaded file
        /// </summary>
        public IBrowserFile NewFile { get; set; }

        #endregion

        #region Injected Members

        /// <summary>
        /// An instance representing the host environment of the application
        /// </summary>
        [Inject]
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        #endregion

        #region Overriden Methdos

        /// <summary>
        /// Loads data on initialization
        /// </summary>
        protected override void LoadData()
        {
            SavedDrafts = _dbContext.SavedDrafts.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            NewDraft.BorrowerCode = _applicationState.BorrowerCode;
        }

        protected override void Setup()
        {
            _dbSet = _dbContext.NoticeAutomations;
        }

        #endregion

        #region Private Methods

        private void Edit(string _id)
        {
            ShouldAddDraft = false;
            int id = int.Parse(_id);
            NewDraft = _dbContext.SavedDrafts.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void Delete(string _id)
        {
            int id = int.Parse(_id);
            DraftModel newDraft = _dbContext.SavedDrafts.Where(f => f.Id == id).First();
            if(newDraft != null)
            {
                _dbContext.Remove(newDraft);
                _dbContext.SaveChanges();
                SavedDrafts = _dbContext.SavedDrafts.ToList();
                NewDraft = new DraftModel();
                NewDraft.BorrowerCode = _applicationState.BorrowerCode;
                StateHasChanged();
            }
        }

        private void ClearDraft()
        {
            ShouldAddDraft = true;
            NewDraft = new DraftModel();
            NewDraft.BorrowerCode = _applicationState.BorrowerCode;
        }

        /// <summary>
        /// Saves a draft in the database
        /// </summary>
        private void SaveDraft()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("Please select a borrower from the account management page");
                return;
            }

            if (ShouldAddDraft)
            {
                _dbContext.SavedDrafts.Add(NewDraft);
            }
            else
            {
                _dbContext.SavedDrafts.Update(NewDraft);
            }
            try
            {
                _dbContext.SaveChanges();
                SavedDrafts = _dbContext.SavedDrafts.ToList();
                NewDraft = new DraftModel();
                NewDraft.BorrowerCode = _applicationState.BorrowerCode;
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);
            }
            StateHasChanged();
        }

        /// <summary>
        /// Fires after the save to reset for next
        /// </summary>
        protected override void SaveResetup()
        {
            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page");
                return;
            }

            NoticeAutomationModel? model = _dbContext.NoticeAutomations.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            if (model == null)
            {
                NewEntry = new NoticeAutomationModel();
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
                ShouldAdd = true;
            }
            else
            {
                NewEntry = model;
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Notices");
                NewFile = new UploadedFile(Path.Combine(folderPath, NewEntry.Notice));
                ShouldAdd = false;
            }
        }

        protected override void OnClear()
        {
        }

        /// <summary>
        /// Fires when the file changes
        /// </summary>
        private async void FileChanged(InputFileChangeEventArgs e)
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page");
                return;
            }

            NewFile = e.File;

            string fileName = NewFile.Name.Replace(" ", "-");
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Notices");

            //Checking if previous file exists
            if (NewEntry.Notice != null)
            {
                //Deleting if a new file has been added
                System.IO.File.Delete(Path.Combine(folderPath, NewEntry.Notice));
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
            NewEntry.Notice = fileName;
        }

        #endregion
    }
}
