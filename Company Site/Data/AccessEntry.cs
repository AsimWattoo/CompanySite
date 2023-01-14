using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class AccessEntry : BaseModel<int>
    {
        /// <summary>
        /// The file to which access is given
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// The id of the contact to whom access is given
        /// </summary>
        [Required(ErrorMessage = "Contact Id is required")]
        public string ContactId { get; set; }

        /// <summary>
        /// The person who gave access to the file
        /// </summary>
        [Required(ErrorMessage = "Username of the person who gave access to a file is required")]
        public string AccessGivenBy { get; set; } = "";

        /// <summary>
        /// The date when the access to the file will end
        /// </summary>
        public DateTime EndDate{ get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates whether the file download is allowed or not
        /// </summary>
        public bool AllowDownload { get; set; } = false;

        /// <summary>
        /// The water marks for the file
        /// </summary>
        public string WaterMarks { get; set; } = string.Empty;
    }
}
