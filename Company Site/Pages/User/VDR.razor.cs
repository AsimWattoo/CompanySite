using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

using NuGet.Common;

using System.IO;

namespace Company_Site.Pages.User
{
    public partial class VDR : ComponentBase
    {
        #region Constant Members

        private const string FOLDER_NAME = "VDR-Files";

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the currently logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The list of file items
        /// </summary>
        private List<FileItem> _fileItems = new List<FileItem>();

        /// <summary>
        /// Indicates whether to show the upload file modal or not
        /// </summary>
        private bool ShowUploadFileModal = false;

        /// <summary>
        /// The new file item which is being created
        /// </summary>
        private FileItem NewItem { get; set; } = new FileItem();

        /// <summary>
        /// The list of files which are being selected for upload
        /// </summary>
        private List<IBrowserFile> Files { get; set; } = new List<IBrowserFile>();

        /// <summary>
        /// Indicates whether to show the input field or not
        /// </summary>
        private bool ShowInputField { get; set; } = false;

        #endregion

        #region Injected Members

        /// <summary>
        /// The injected db context
        /// </summary>
        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; }

        [Inject]
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the item is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            LoadData();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            if (UserId.HasValue)
            {
                //The list of files for the user id
                _fileItems = _dbContext.FileItems.ToList();
                foreach (FileItem fileItem in _fileItems)
                {
                    string folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);
                    fileItem.File = new UploadedFile(Path.Join(folderPath, fileItem.FileName));
                }
            }
        }

        /// <summary>
        /// Shows the form to upload file
        /// </summary>
        private void ShowUploadForm()
        {
            ShowUploadFileModal = true;
        }

        /// <summary>
        /// Initiates the file uploading
        /// </summary>
        private async void UploadFiles()
        {
            await JS.InvokeVoidAsync("ClickElement", "#fileInput");
        }

        /// <summary>
        /// Fires when the input files changes
        /// </summary>
        /// <param name="e"></param>
        private void FileChanged(InputFileChangeEventArgs e)
        {
            IReadOnlyList<IBrowserFile> files = e.GetMultipleFiles();
            foreach (IBrowserFile file in files)
            {
                Files.Add(file);
            }
            ShowInputField = false;
        }

        /// <summary>
        /// Removes the specified file from the upload file list
        /// </summary>
        /// <param name="file"></param>
        private void RemoveFile(IBrowserFile file)
        {
            Files.Remove(file);
        }

        /// <summary>
        /// Hides the upload form
        /// </summary>
        private void HideUploadForm()
        {
            NewItem = new FileItem();
            ShowUploadFileModal = false;
        }

        /// <summary>
        /// Fires when the mouse is dragged over the upload box
        /// </summary>
        /// <param name="e"></param>
        private void DraggedOverDropPoint(DragEventArgs e)
        {
            ShowInputField = true;
        }

        /// <summary>
        /// Submits the files
        /// </summary>
        private async void SubmitFiles()
        {
            //Getting the User Account
            if (!UserId.HasValue)
                return;


            string folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (IBrowserFile file in Files)
            {
                FileItem fileItem = new FileItem();
                FileInfo fileInfo = new FileInfo(file.Name);
                fileItem.FileName = $"{fileInfo.Name.Replace(fileInfo.Extension, "")}$${DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")}{fileInfo.Extension}";
                fileItem.CreationDate = DateTime.Now;
                fileItem.UserId = UserId.Value;
                _dbContext.FileItems.Add(fileItem);

                //Uploading File
                using (BinaryWriter writer = new BinaryWriter(new FileStream(Path.Join(folderPath, fileItem.FileName), FileMode.Create)))
                {
                    await file.OpenReadStream(50 * 1024 * 1024)
                        .CopyToAsync(writer.BaseStream);
                    writer.Flush();
                }
            }
            _dbContext.SaveChanges();
            Files.Clear();
            ShowUploadFileModal = false;
            LoadData();
            StateHasChanged();
        }

        /// <summary>
        /// Converts the size to MB
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private double ToMb(long size)
        {
            return Math.Round(size / (1024.0 * 1024.0), 2);
        }

        #endregion
    }
}
