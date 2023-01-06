using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class Calendar : ComponentBase
    {
        #region Public Properties

        /// <summary>
        /// The id of the currently logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The names of days in a week
        /// </summary>
        private string[] DayNames = new string[]
        {
            "Sun",
            "Mon",
            "Tue",
            "Wed",
            "Thu",
            "Fri",
            "Sat"
        };

        /// <summary>
        /// The weekends
        /// </summary>
        private string[] Weekends = new string[]
        {
            "Sun",
            "Sat"
        };

        /// <summary>
        /// The days in the month
        /// </summary>
        private List<(string, int)> Days = new List<(string, int)>();

        /// <summary>
        /// The tasks per day for the entire month
        /// </summary>
        private Dictionary<int, List<WorkTask>> _tasks = new Dictionary<int, List<WorkTask>>();

        #endregion

        #region Injected Members

        /// <summary>
        /// The application db context inject in the application
        /// </summary>
        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the control is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            LoadData();
        }

        #endregion

        #region Public Methods

        public void UpdateState()
        {
            ClearData();
            LoadData();
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads data in their respective lists
        /// </summary>
        private void LoadData()
        {
            int month = 1;
            int numberOfDays = DateTime.DaysInMonth(2023, month);
            for (int i = 1; i <= numberOfDays; i++)
            {
                DateTime date = new DateTime(2023, month, i);
                Days.Add((date.ToString("ddd"), i));
            }
            //Getting tasks for the current month
            List<int> taskForCurrentUser = _dbContext.TaskAssignments.Where(f => f.UserId == UserId).Select(f => f.TaskId).Distinct().ToList();
            List<WorkTask> tasks = _dbContext.Tasks.Where(f => f.ReminderDate.Month == month && taskForCurrentUser.Contains(f.Id)).ToList();
            //Adding tasks for the days
            foreach (WorkTask task in tasks)
            {
                if (task.ShouldRemind)
                {
                    if (!_tasks.ContainsKey(task.ReminderDate.Day))
                        _tasks.Add(task.ReminderDate.Day, new List<WorkTask>());
                    _tasks[task.ReminderDate.Day].Add(task);
                }
            }
        }

        /// <summary>
        /// Clears the data
        /// </summary>
        private void ClearData()
        {
            Days.Clear();
            _tasks.Clear();
        }

        #endregion

    }
}
