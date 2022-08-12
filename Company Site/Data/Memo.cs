using Company_Site.Enum;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class Memo
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

        /// <summary>
        /// The department of the user
        /// </summary>
        [Required]
        public string Department { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        public int ThroughId { get; set; }

        [Required]
        public int FromId { get; set; }

        [Required]
        public string Text { get; set; }

        public string FilesId { get; set; }

        public int CommentsId { get; set; }

        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        #endregion

    }
}
