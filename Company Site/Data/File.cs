using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class File
    {

        #region Public Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FileId { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string MemoId { get; set; }

        #endregion

    }
}
