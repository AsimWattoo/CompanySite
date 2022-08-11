using System.ComponentModel.DataAnnotations;

namespace Company_Site.DB
{
	public class Memo
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
        public DateTime Date { get; set; }

		[Required]
		public bool Financial { get; set; }

		[Required]
		public string Type { get; set; }

		[Required]
		public int Amount { get; set; }

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

		#endregion

	}
}
