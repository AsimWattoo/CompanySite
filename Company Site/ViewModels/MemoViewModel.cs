using Company_Site.Data;
using Company_Site.Enum;

using Microsoft.AspNetCore.Components.Forms;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.ViewModels
{
	public class MemoViewModel
	{
        #region Public Properties

        [Required]
        [Key]
        public string MemoNumber { get; set; }

        [Required]
        public string CaseName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public bool Financial { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Periodicity { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public DateTime ValidTill { get; set; } = DateTime.Now;

        [Required]
        public string Branch { get; set; }

        [Required]
        public string Vendor { get; set; }

        public string? Frequency { get; set; }

        [Required]
        public List<string> To { get; set; } = new List<string>();

        [Required]
        public List<string> Through { get; set; } = new List<string>();

        [Required]
        public List<string> From { get; set; } = new List<string>();

        public string Text { get; set; }

        public List<IBrowserFile> Files { get; set; } = new List<IBrowserFile>();

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        /// <summary>
        /// Indicates whether the memo is saved as draft or not
        /// </summary>
        public bool IsDraft { get; set; } = false;

        public int BorrowerCode { get; set; }

        #endregion
    }
}
