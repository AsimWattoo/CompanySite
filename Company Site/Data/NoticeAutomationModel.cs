using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class NoticeAutomationModel : BaseModel<int>
    {
        /// <summary>
        /// The id of the account to which this notice automation model belongs to
        /// </summary>
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Notice Type is required")]
        public string NoticeType { get; set; }

        /// <summary>
        /// The notice format
        /// </summary>
        public string? NoticeFormat { get; set; }

        public string? FormatName { get; set; }
    }
}
