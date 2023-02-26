using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Company_Site.Pages.User
{
    public partial class VDR : ComponentBase
    {
        #region Constant Members

        private const string FOLDER_NAME = "VDR-Files";

        #endregion

        #region Folder Properties

        /// <summary>
        /// The actual path to obtain the files
        /// </summary>
        private string? _opennedFolderPath = "";

        /// <summary>
        /// The path to show to the user
        /// </summary>
        private string? _displayedPath = "";

        /// <summary>
        /// Indicates whether the folder dialog box has been displayed or not
        /// </summary>
        private bool isFolderDialogShown = false;

        /// <summary>
        /// The list of errors for the folder dialog
        /// </summary>
        private List<string> _folderErrors = new List<string>();

        /// <summary>
        /// The name of the folder
        /// </summary>
        private string folderName { get; set; }

        /// <summary>
        /// Shows the dialog box to create a folder
        /// </summary>
        private void ShowFolderDialog()
        {
            isFolderDialogShown = true;
            _folderErrors.Clear();
        }

        /// <summary>
        /// Confirms the creation of the folder
        /// </summary>
        private void CreateFolder()
        {
            _folderErrors.Clear();

            if (string.IsNullOrEmpty(folderName))
            {
                _folderErrors.Add("Folder name must not be empty");
                return;
            }
            try
            {
                string newName = $"{folderName}$${DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")}";
                string folderPath = "";

                if(string.IsNullOrEmpty(_opennedFolderPath))
                    folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, newName);
                else
                    folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, _opennedFolderPath, newName);

                Directory.CreateDirectory(folderPath);
                _dbContext.FileItems.Add(new FileItem()
                {
                    FileName = newName,
                    CreationDate = DateTime.Now,
                    Type = FileType.Folder,
                    UserId = UserId,
                    FolderPath = _opennedFolderPath
                });
                _dbContext.SaveChanges();
                folderName = null;
                isFolderDialogShown = false;
                LoadData();
            }
            catch(Exception exc)
            {
                _folderErrors.Add(exc.Message);
            }
        }

        /// <summary>
        /// Cancels the creation of the folder
        /// </summary>
        private void CancelFolderDialog()
        {
            _folderErrors.Clear();
            isFolderDialogShown = false;
        }

        /// <summary>
        /// Fires when the folder is clicked
        /// </summary>
        /// <param name="name">The name of the folder being clicked</param>
        private void FolderClicked(string name)
        {
            if (string.IsNullOrEmpty(_opennedFolderPath))
            {
                _displayedPath = name.Split("$$").First();
                _opennedFolderPath = name;
            }
            else
            {
                _displayedPath += $"\\{name.Split("$$").First()}";
                _opennedFolderPath += $"\\{name}";
            }
            LoadData();
        }

        /// <summary>
        /// Navigates the path in backwardly
        /// </summary>
        private void NavigateFolderBack()
        {
            if (_opennedFolderPath == null)
                return;

            string[] path = _opennedFolderPath.Split("\\");
            if(path.Length > 2)
            {
                _opennedFolderPath = path[0];
                _displayedPath = path[0].Split("$$").First();
                for (int i = 1; i < path.Length - 1; i++)
                {
                    _opennedFolderPath += $"\\{path[i]}";
                    _displayedPath += $"\\{path[i].Split("$$").First()}";
                }
            }
            else if(path.Length == 2)
            {
                _opennedFolderPath = path[0];
                _displayedPath = path[0].Split("$$").First();
            }
            else
            {
                _displayedPath = null;
                _opennedFolderPath = null;
            }
            LoadData();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The id of the currently logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public string UserId { get; set; }

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

        /// <summary>
        /// The total progress of file uploading
        /// </summary>
        private double _progress { get; set; } = 0;

        /// <summary>
        /// Indicates whether the uploading has started or not
        /// </summary>
        private bool isUploading { get; set; }

        /// <summary>
        /// The errors which have occurred
        /// </summary>
        private List<string> _errors { get; set; } = new List<string>();

        /// <summary>
        /// Indicates whether the settings form is shown or not
        /// </summary>
        private bool SettingsFormShown = false;

        /// <summary>
        /// The id of the file which is being openned in settings
        /// </summary>
        private int _fileId = -1;

        /// <summary>
        /// The list of water marsks
        /// </summary>
        private List<WaterMark> WaterMarks = new List<WaterMark>()
        {
            new WaterMark(){ Id = 1, Name = "Email"},
            new WaterMark(){ Id = 2, Name = "Ip Address"},
            new WaterMark(){ Id = 3, Name = "Timestamp"},
        };

        /// <summary>
        /// The list of selected water marks
        /// </summary>
        private List<int> SelectedWaterMarks = new List<int>();

        /// <summary>
        /// Indicates whether the delete confirmation should be visible or not
        /// </summary>
        private bool IsDeleteConfirmationVisible { get; set; } = false;

        #endregion

        #region Access Table Properties

        /// <summary>
        /// The new access entry
        /// </summary>
        private AccessEntry NewAccessEntry { get; set; } = new AccessEntry();

        /// <summary>
        /// The list of contacts
        /// </summary>
        private List<ContactsModel> Contacts { get; set; } = new List<ContactsModel>();

        /// <summary>
        /// The id of the selected contact
        /// </summary>
        private string? SelectedContactId { get; set; } = null;

        /// <summary>
        /// The list of access entries for the files
        /// </summary>
        private List<AccessEntry> AccessEntries { get; set; } = new List<AccessEntry>();

        /// <summary>
        /// Headers for the access table
        /// </summary>
        private Dictionary<string, Func<AccessEntry, string>> AccessTableHeaders { get; set; } = new Dictionary<string, Func<AccessEntry, string>>()
        {
            ["Contact Name"] = t => t.ContactId.ToString(),
            ["Access Given By"] = t => t.AccessGivenBy,
            ["End Date"] = t => t.EndDate.ToString("dd-MMM-yyyy"),
            ["Allow Download"] = t => t.AllowDownload.ToString(),
        };

        /// <summary>
        /// Returns value for the headers
        /// </summary>
        /// <param name="header"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ValueCallback(string header, AccessEntry item)
        {
            switch (header)
            {
                case "Contact Name":
                    ContactsModel contact = _dbContext.Contacts.Where(f => f.ContactId == item.ContactId).First();
                    return $"{contact.FirstName} {contact.LastName}";
                case "Access Given By":
                case "End Date":
                case "Allow Download":
                    return AccessTableHeaders[header].Invoke(item);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Returns the id of the access entry
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private int GetAccessId(AccessEntry t) => t.Id; 

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
            Contacts = _dbContext.Contacts.ToList();
            LoadData();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            if (UserId != null)
            {
                //The list of files for the user id
                List<FileItem> items = _dbContext.FileItems.ToList();

                if (!string.IsNullOrEmpty(_opennedFolderPath))
                {
                    items = items.Where(f => f.FolderPath == _opennedFolderPath).ToList();
                }
                else
                {
                    items = items.Where(f => string.IsNullOrEmpty(f.FolderPath)).ToList();
                }

                _fileItems = items;
                foreach (FileItem fileItem in _fileItems)
                {
                    //Only Load files. Leave the folders
                    if(fileItem.Type == FileType.File)
                    {
                        string folderPath = "";

                        if (string.IsNullOrEmpty(fileItem.FolderPath))
                            folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);
                        else
                            folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, fileItem.FolderPath);

                        fileItem.File = new UploadedFile(Path.Join(folderPath, fileItem.FileName));
                    }
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
            if (UserId == null)
                return;

            _errors.Clear();
            isUploading = true;
            string folderPath = "";

            if (string.IsNullOrEmpty(_opennedFolderPath))
            {
                folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME);
            }
            else
            {
                folderPath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, _opennedFolderPath);
            }

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //Calculating total upload size
            long totalUploadSize = 0;
            int maxAllowedSize = 50 * 1024 * 1024;
            foreach (IBrowserFile file in Files)
            {
                if(file.Size > maxAllowedSize)
                {
                    _errors.Add($"File {file.Name} is greater than 50Mb.");
                    isUploading = false;
                    return;
                }
                totalUploadSize += file.Size;
            }

            foreach (IBrowserFile file in Files)
            {
                FileItem fileItem = new FileItem();
                FileInfo fileInfo = new FileInfo(file.Name);
                fileItem.FileName = $"{fileInfo.Name.Replace(fileInfo.Extension, "")}$${DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")}{fileInfo.Extension}";
                fileItem.CreationDate = DateTime.Now;
                fileItem.UserId = UserId;
                fileItem.FolderPath = _opennedFolderPath;
                _dbContext.FileItems.Add(fileItem);

                //Uploading File
                using (BinaryWriter writer = new BinaryWriter(new FileStream(Path.Join(folderPath, fileItem.FileName), FileMode.Create)))
                {
                    await file.OpenReadStream(maxAllowedSize)
                        .CopyToAsync(writer.BaseStream);
                    writer.Flush();
                }
                _progress += (file.Size / (double)totalUploadSize) * 100;
                StateHasChanged();
            }
            _dbContext.SaveChanges();
            Files.Clear();
            ShowUploadFileModal = false;
            LoadData();
            isUploading = false;
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

        /// <summary>
        /// Opens settings for the specified file id
        /// </summary>
        /// <param name="id"></param>
        private void OpenSettings(int id)
        {
            _fileId = id;
            SettingsFormShown = true;
            NewAccessEntry = new AccessEntry();
            Clear();
            LoadFileAccessEnteries(id);
        }

        /// <summary>
        /// Deletes the file with the specified id
        /// </summary>
        /// <param name="id">The id of the file to remove</param>
        private void DeleteFile()
        {
            if (_fileId == -1)
                return;
            IsDeleteConfirmationVisible = true;
        }

        /// <summary>
        /// Deletes the record
        /// </summary>
        private void DeleteRecord()
        {
            FileItem item = _dbContext.FileItems.Where(f => f.Id == _fileId).First();
            string filePath = "";

            if(string.IsNullOrEmpty(item.FolderPath))
                filePath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, item.FileName);
            else
                filePath = Path.Join(_webHostEnvironment.WebRootPath, FOLDER_NAME, item.FolderPath, item.FileName);

            if (item.Type == FileType.File)
                System.IO.File.Delete(filePath);
            else
                Directory.Delete(filePath, true);
            _dbContext.FileItems.Remove(item);
            _dbContext.SaveChanges();
            SettingsFormShown = false;
            IsDeleteConfirmationVisible = false;
            LoadData();
            StateHasChanged();
        }

        /// <summary>
        /// Fires when the contact is changed
        /// </summary>
        /// <param name="account"></param>
        private void ContactChanged(object contact)
        {
            if(contact is string contactId)
            {
                SelectedContactId = contactId;
            }
        }

        /// <summary>
        /// Cancels the delete confirmation
        /// </summary>
        private void CancelDeleteConfirmation()
        {
            IsDeleteConfirmationVisible = false;
        }

        #endregion

        #region Access Entry Methods

        /// <summary>
        /// Loads enteries for the file access
        /// </summary>
        private void LoadFileAccessEnteries(int fileId)
        {
            AccessEntries = _dbContext.FileAccessEntries.Where(f => f.FileId == fileId).ToList();
        }

        /// <summary>
        /// Saves an access entry
        /// </summary>
        private void SaveAccessEntry()
        {
            if (UserId == null || SelectedContactId == null)
                return;

            var user = _dbContext.Users.Where(f => f.Id == UserId).First();
            NewAccessEntry.AccessGivenBy = user.UserName;
            NewAccessEntry.ContactId = SelectedContactId;
            NewAccessEntry.WaterMarks = "";
            foreach(int waterMark in SelectedWaterMarks)
            {
                NewAccessEntry.WaterMarks += $"{waterMark}-";
            }
            NewAccessEntry.WaterMarks = NewAccessEntry.WaterMarks.Trim('-');
            NewAccessEntry.FileId = _fileId;
            _dbContext.FileAccessEntries.Add(NewAccessEntry);
            _dbContext.SaveChanges();
            LoadFileAccessEnteries(_fileId);
            Clear();
            StateHasChanged();
        }

        private void Clear()
        {
            NewAccessEntry = new AccessEntry();
            SelectedContactId = null;
            SelectedWaterMarks = new List<int>();
        }

        /// <summary>
        /// Removes access entry
        /// </summary>
        private List<AccessEntry> RemoveAccessEntry(int id)
        {
            AccessEntry entry = _dbContext.FileAccessEntries.Where(f => f.Id == id).First();
            _dbContext.FileAccessEntries.Remove(entry);
            _dbContext.SaveChanges();
            LoadFileAccessEnteries(_fileId);
            StateHasChanged();
            return AccessEntries;
        }

        #endregion
    }
}
