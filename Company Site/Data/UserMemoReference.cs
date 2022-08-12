using Company_Site.Enum;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class UserMemoReference
    {

        #region Public Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string MemoId { get; set; }

        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The status of memo from the referenced person.
        /// </summary>
        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        #endregion

    }
}
