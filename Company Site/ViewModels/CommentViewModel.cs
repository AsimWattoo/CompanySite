using Company_Site.Data;

namespace Company_Site.ViewModels
{
	public class CommentViewModel
	{

        #region Public Properties

        public int CommentId { get; set; }

        public string MemoNumber { get; set; }

        public string UserName { get; set; }

        public string CommentText { get; set; }

        public bool ShowComment => !CommentText.Equals("<p><br></p>");

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="com"></param>
        /// <param name="userName"></param>
        public CommentViewModel(Comment com, string userName)
        {
            FromComment(com, userName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads data from a comment
        /// </summary>
        /// <param name="comment"></param>
        public void FromComment(Comment comment, string userName)
        {
            CommentId = comment.CommentId;
            MemoNumber = comment.MemoId;
            UserName = userName;
            CommentText = comment.CommentText;
        }

        #endregion

    }
}
