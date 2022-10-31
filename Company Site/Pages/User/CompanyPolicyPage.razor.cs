using Company_Site.Data;
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

        [Inject]
        private IJSRuntime JS { get; set; }

        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        private string DocumentUrl { get; set; } = "/Documents/document.pdf";

        #endregion

        #region Private Members

        /// <summary>
        /// Fires when the PDF file changes
        /// </summary>
        private async Task PdfFileChanged(InputFileChangeEventArgs e)
        {
            File = e.File;
            using (BinaryWriter writer = new BinaryWriter(new FileStream(Path.Combine(WebHostEnvironment.WebRootPath, "Documents", "document.pdf"), FileMode.Create)))
            {
                await File.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(writer.BaseStream);
                writer.Flush();
            }
            await JS.InvokeVoidAsync("LoadPdf", "#pdf-container", "/Documents/document.pdf");
        }

        #endregion

    }
}
