using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class Documentslist : BaseAddPage<DocumentsListsModel, int>, ITable<DocumentsListsModel, int>
    {
        #region Public Properties

        /// <summary>
        /// The headers for the table
        /// </summary>
        public Dictionary<string, Func<DocumentsListsModel, string>> Headers { get; set; } = new Dictionary<string, Func<DocumentsListsModel, string>>()
        {
            ["Title"] = p => p.Title,
            ["Description"] = p => p.Description,
            ["Type"] = p => p.Type,
            ["Document Date"] = p => p.Document_Date.ToString("dd-MMM-yyyy"),
        };

        #endregion

        #region Private Members

        private const string FOLDER_NAME = "DocumentsList";

        private string? PreviousDocumentName = null;

        /// <summary>
        /// The browser file
        /// </summary>
        private IBrowserFile _file { get; set; }

        #endregion

        #region Injected Members

        [Inject]
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Setups the necessary items for the page
        /// </summary>
        protected override void Setup()
        {
            _dbSet = _dbContext.DocumentLists;
        }

        #endregion

        #region Public Methods

        public int GetId(DocumentsListsModel t) => t.Id;

        public bool SearchItem(DocumentsListsModel e)
        {
            return e.Title.Contains(_text) || e.Description.Contains(_text) || e.Type.Contains(_text);
        }

        private string _text;

        public List<DocumentsListsModel> Search(List<DocumentsListsModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        /// <summary>
        /// Fires when the file changes
        /// </summary>
        /// <param name="e"></param>
        private void FileChanged(InputFileChangeEventArgs e)
        {
            NewEntry.Document = $"{e.File.Name}";
            _file = e.File;
        }

        protected override bool SaveSetup()
        {
            return false;
        }

        private bool SaveEntry()
        {
            if (ShouldAdd)
                _dbSet.Add(NewEntry);
            else
                _dbSet.Update(NewEntry);
            return true;
        }

        protected override async Task<bool> SaveSetup(DocumentsListsModel model)
        {
            if (SaveEntry())
            {
                if (string.IsNullOrEmpty(NewEntry.Document))
                {
                    _errors.Add("Please select a document to upload");
                    return false;
                }

                string folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Join(folderPath, _file.Name);
                if (ShouldAdd)
                {
                    using (StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.Create)))
                    {
                        //Allowing 50 MB File Read
                        await _file.OpenReadStream(50 * 1024 * 1024)
                        .CopyToAsync(writer.BaseStream);
                        writer.Flush();
                    }
                    return true;
                }
                else
                {
                    if (PreviousDocumentName != NewEntry.Document)
                    {
                        System.IO.File.Delete(filePath);

                        filePath = Path.Join(folderPath, _file.Name);
                        using (StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.Create)))
                        {
                            //Allowing 50 MB File Read
                            await _file.OpenReadStream(50 * 1024 * 1024)
                                .CopyToAsync(writer.BaseStream);
                            writer.Flush();
                        }
                    }
                    return true;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Cleans up the file for the record
        /// </summary>
        /// <param name="item"></param>
        protected override void OnDelete(DocumentsListsModel item)
        {
            string folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);
            string filePath = Path.Join(folderPath, item.Document);
            System.IO.File.Delete(filePath);
        }

        public override void OnEdit(int id)
        {
            DocumentsListsModel model = _dbContext.DocumentLists.Where(f => f.Id == NewEntry.Id).First();
            PreviousDocumentName = model.Document;
        }

        #endregion
    }
}
