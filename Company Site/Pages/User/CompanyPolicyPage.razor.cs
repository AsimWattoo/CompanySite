using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

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

        #endregion

        #region Injected Members

        [Inject]
        private IJSRuntime JS { get; set; }

        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the component is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            Reset();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                RenderFile();
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Clears the values to the previous values
        /// </summary>
        private void Reset()
        {
            if (_dbContext.Policies.Any())
            {
                NewPolicy = _dbContext.Policies.First();
                File = new UploadedFile(NewPolicy.FilePath);
                RenderFile();
                StateHasChanged();
            }
        }

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async Task PdfFileChanged(InputFileChangeEventArgs e)
        {
            File = e.File;
            NewPolicy.FilePath = Path.Combine(WebHostEnvironment.WebRootPath, "Documents", "document.pdf");
            using (BinaryWriter writer = new BinaryWriter(new FileStream(NewPolicy.FilePath, FileMode.Create)))
            {
                await File.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            RenderFile();
        }

        private async void RenderFile()
        {
            await JS.InvokeVoidAsync("LoadPdf", "#pdf-container", "/Documents/document.pdf");
        }

        /// <summary>
        /// Saves the data to the database
        /// </summary>
        private void Save()
        {
            if (_dbContext.Policies.Any())
            {
                _dbContext.Policies.Update(NewPolicy);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.Policies.Add(NewPolicy);
                _dbContext.SaveChanges();
            }
        }

        #endregion

    }
}
