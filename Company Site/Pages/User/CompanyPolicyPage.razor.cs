using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;
using Company_Site.Pages.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

using System.Data;
using System.Drawing.Imaging;

namespace Company_Site.Pages.User
{
    public partial class CompanyPolicyPage : ComponentBase
    {

        #region Private Members

        /// <summary>
        /// The Company policy
        /// </summary>
        private CompanyPolicy NewPolicy { get; set; } = new CompanyPolicy();

        /// <summary>
        /// The Browser File
        /// </summary>
        private IBrowserFile File { get; set; }

        /// <summary>
        /// Indicates the current mode of the application
        /// </summary>
        private bool ShouldAdd = true;

        /// <summary>
        /// The list of company policies on the system
        /// </summary>
        private List<CompanyPolicy> companyPolicies= new List<CompanyPolicy>();

        private string PreviousFile = "";

        #endregion

        #region Injected Members

        [Inject]
        private IJSRuntime JS { get; set; }

        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the component is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            companyPolicies = _dbContext.Policies.ToList();
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async Task PdfFileChanged(InputFileChangeEventArgs e)
        {
            File = e.File;
            NewPolicy.FilePath = $"document-{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}.pdf";
            string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "Documents", NewPolicy.FilePath);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Create)))
            {
                await File.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            RenderFile(NewPolicy.FilePath);
        }

        private async void RenderFile(string filePath)
        {
            if(filePath == "")
            {
                await JS.InvokeVoidAsync("LoadPdf", "#pdf-container", filePath);
            }
            else
                await JS.InvokeVoidAsync("LoadPdf", "#pdf-container", $"/Documents/{filePath}");
        }

        /// <summary>
        /// Clears the text
        /// </summary>
        private void Clear()
        {
            if(ShouldAdd && !String.IsNullOrEmpty(NewPolicy.FilePath))
                DeleteFile(NewPolicy.FilePath);
            ShouldAdd = true;
            RenderFile("");
            NewPolicy = new CompanyPolicy();
        }

        /// <summary>
        /// Saves the data to the database
        /// </summary>
        private void Save()
        {
            if (ShouldAdd)
            {
                _dbContext.Policies.Add(NewPolicy);
                _dbContext.SaveChanges();
                companyPolicies = _dbContext.Policies.ToList();
                NewPolicy = new CompanyPolicy();
            }
            else
            {
                DeleteFile(PreviousFile);
                _dbContext.Policies.Update(NewPolicy);
                _dbContext.SaveChanges();
                companyPolicies = _dbContext.Policies.ToList();
                NewPolicy = new CompanyPolicy();
            }
            RenderFile("");
            ShouldAdd = true;
            _navigationManager.NavigateTo(_navigationManager.Uri, true);
        }

        private void DeleteFile(string fileName)
        {
            string path = Path.Combine(WebHostEnvironment.WebRootPath, "Documents", fileName);
            FileInfo info = new FileInfo(path);
            if(info.Exists)
                System.IO.File.Delete(path);
        }

        /// <summary>
        /// Fires when the edit button is clicked
        /// </summary>
        private void OnEdit(string id)
        {
            int policyId;
            if(int.TryParse(id, out policyId))
            {
                NewPolicy = _dbContext.Policies.Where(f => f.Id == policyId).First();
                ShouldAdd = false;
                RenderFile(NewPolicy.FilePath);
                PreviousFile = NewPolicy.FilePath;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Fires when the record is requested to be deleted
        /// </summary>
        /// <param name="id"></param>
        private void OnDelete(string id)
        {
            try
            {
                int policyId;
                if (int.TryParse(id, out policyId))
                {
                    CompanyPolicy policy = _dbContext.Policies.Where(f => f.Id == policyId).First();
                    _dbContext.Policies.Remove(policy);
                    _dbContext.SaveChanges();
                    companyPolicies = _dbContext.Policies.ToList();
                    DeleteFile(policy.FilePath);
                    NewPolicy = new CompanyPolicy();
                    ShouldAdd = true;
                    _navigationManager.NavigateTo(_navigationManager.Uri, true);
                    StateHasChanged();
                }
            }
            catch { }
        }

        /// <summary>
        /// Searches and returns the filtered records
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<CompanyPolicy> OnSearch(string text)
        {
            return companyPolicies.Where(f => f.PolicyName.Contains(text)).ToList();
        }

        #endregion

    }
}
