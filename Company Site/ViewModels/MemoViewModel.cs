using Company_Site.Enum;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.ViewModels
{
	public class MemoViewModel
	{
        #region Public Properties

        public int SerialNumber { get; set; }

        [Required]
        [Key]
        public string MemoNumber { get; set; }

        [Required]
        public string CaseName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Financial { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public List<string> To { get; set; }

        [Required]
        public List<string> Through { get; set; }

        [Required]
        public List<string> From { get; set; }

        [Required]
        public string Text { get; set; }

        public List<string> FilesId { get; set; }

        public List<string> Comments { get; set; }

        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        #endregion
    }
}
