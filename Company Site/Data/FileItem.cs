using Company_Site.Base;
using Company_Site.Enum;

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
        /// The type of the file
        /// </summary>
        public FileType Type { get; set; } = FileType.File;

        /// <summary>
        /// The date and time when the file is created
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The person who created the file
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// The path of the folder for the file
        /// </summary>
        public string? FolderPath { get; set; }

        #endregion
    }
}
