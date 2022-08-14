using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class File
    {

        #region Public Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string MemoId { get; set; }

        #endregion

    }
}
