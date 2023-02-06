using Company_Site.Base;

namespace Company_Site.Data
{
    public class TaskAssignment : BaseModel<int>
    {
        #region Public Properties

        /// <summary>
        /// The id of the task
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// The id of the user id
        /// </summary>
        public string UserId { get; set; }

        #endregion
    }
}
