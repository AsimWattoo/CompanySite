using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentId { get; set; }

        [Required]
        public string MemoId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string CommentText { get; set; }

    }
}
