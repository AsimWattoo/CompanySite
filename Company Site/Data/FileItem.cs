using Company_Site.Base;

using Microsoft.AspNetCore.Components.Forms;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace Company_Site.Data
{
    public class FileItem : BaseModel<int>
    {
        #region Public Properties

        [Required( ErrorMessage = "File name is required")]
        public string FileName { get; set; } = "";

        /// <summary>
        /// The browser file
        /// </summary>
        [NotMapped]
        public IBrowserFile? File { get; set; }

        /// <summary>
        /// The date and time when the file is created
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The person who created the file
        /// </summary>
        public int UserId { get; set; }

        #endregion
    }
}
