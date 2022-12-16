using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ApprovalsAndMemoModel : BaseModel<int>
    {
        /// <summary>
        /// The borrower code to which this entry is linked
        /// </summary>
        [Required(ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Approval Date  is required")]
        public DateTime Approval_Date   { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Type is required")]
        public string Type    { get; set; }

        [Required(ErrorMessage = "Committee is required")]
        public string Committee { get; set; }

        [Required(ErrorMessage = "Details is required")]
        public string Details    { get; set; }

        [Required(ErrorMessage = "File is required")]
        public string FilePath    { get; set; }
    }
}
