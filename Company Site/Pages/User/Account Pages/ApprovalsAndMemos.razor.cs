using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Helpers;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class ApprovalsAndMemos : BaseAddPage<ApprovalsAndMemoModel, int>, ITable<ApprovalsAndMemoModel, int>
    {
        #region Public Properties

        public Dictionary<string, Func<ApprovalsAndMemoModel, string>> Headers { get; set; } = new Dictionary<string, Func<ApprovalsAndMemoModel, string>>()
        {
            ["Date"] = p => p.Approval_Date.ToString(),
            ["committee"] = p => p.Committee,
            ["Discription"] = p => p.Details,
            ["Supporting"] = p => p.FilePath.Split('-').Last(),
        };

        public IBrowserFile NewFile { get; set; }

        #endregion

        #region Injected Members

        [Inject]
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.ApprovalsAndMemoModels;
        }

        #endregion

        #region Public Methods

        public int GetId(ApprovalsAndMemoModel t) => t.Id;

        public bool SearchItem(ApprovalsAndMemoModel e)
        {
            return e.Approval_Date.ToString().Contains(_text) || e.Committee.Contains(_text) || e.Details.Contains(_text);
        }

        private string _text;

        public List<ApprovalsAndMemoModel> Search(List<ApprovalsAndMemoModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async void SupportingFileChanged(InputFileChangeEventArgs e)
        {
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
            fileName = $"{Guid.NewGuid()}-{fileName}";
            string path = Path.Combine(folderPath, fileName);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
            {
                await NewFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            NewEntry.FilePath = fileName;
        }

        public override List<ApprovalsAndMemoModel> DeleteRecord(int id)
        {
            ApprovalsAndMemoModel model = _dbContext.ApprovalsAndMemoModels.Where(f => f.Id == id).First();
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Approvals_Docs");
            System.IO.File.Delete(Path.Combine(folderPath, model.FilePath));
            return base.DeleteRecord(id);
        }

        #endregion
    }
}
