using Company_Site.Enum;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class UserMemoReference
    {

        #region Public Properties

        public int Id { get; set; }

        public string MemoId { get; set; }

        public string UserId { get; set; }

        [Required]
        public DateTime ApprovalDate { get; set; }

        /// <summary>
        /// The status of memo from the referenced person.
        /// </summary>
        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        #endregion

    }
}
