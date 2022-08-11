using System.ComponentModel.DataAnnotations;

namespace Company_Site.DB
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string MemoId { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public string CommentText { get; set; }

	}
}
