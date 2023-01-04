using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class WorkTask : BaseModel<int>
    {
        /// <summary>
        /// The title of the task
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the user should be reminded of the task or not
        /// </summary>
        public bool ShouldRemind { get; set; } = false;

        /// <summary>
        /// The date when the reminder is to be given to the user
        /// </summary>
        public DateTime ReminderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The time when the reminder is to be given to the user
        /// </summary>
        public DateTime ReminderTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Indicates whether the task is completed or not
        /// </summary>
        public bool Completed { get; set; } = false;
    }
}
