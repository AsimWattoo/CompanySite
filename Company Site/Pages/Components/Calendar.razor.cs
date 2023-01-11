using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using System.Collections;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        /// The list of months
        /// </summary>
        private string[] Months = new string[]
        {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "June",
            "July",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec",
        };

        /// <summary>
        /// The days in the month
        /// </summary>
        private List<(string, int)> Days = new List<(string, int)>();

        /// <summary>
        /// The tasks per day for the entire month
        /// </summary>
        private Dictionary<int, List<WorkTask>> _tasks = new Dictionary<int, List<WorkTask>>();

        /// <summary>
        /// The start of the decade
        /// </summary>
        private int DecadeStart = 0;

        /// <summary>
        /// The end of the decade
        /// </summary>
        private int DecadeEnd = 0;

        /// <summary>
        /// The current value
        /// </summary>
        private DateTime Date = DateTime.Now;

        /// <summary>
        /// The current mode of the calender
        /// </summary>
        private CalendarMode Mode = CalendarMode.Month;

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
            int year = Date.Year % 10;
            DecadeStart = Date.Year - year;
            DecadeEnd = DecadeStart + 10;
            LoadData(Date);
        }

        #endregion

        #region Public Methods

        public void UpdateState()
        {
            LoadData(Date);
            StateHasChanged();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads data in their respective lists
        /// </summary>
        private void LoadData(DateTime date)
        {
            ClearData();
            int numberOfDays = DateTime.DaysInMonth(date.Year, date.Month);
            for (int i = 1; i <= numberOfDays; i++)
            {
                DateTime d = new DateTime(date.Year, date.Month, i);
                Days.Add((d.ToString("ddd"), i));
            }
            //Getting tasks for the current month
            List<int> taskForCurrentUser = _dbContext.TaskAssignments.Where(f => f.UserId == UserId).Select(f => f.TaskId).Distinct().ToList();
            List<WorkTask> tasks = _dbContext.Tasks.Where(f => f.ReminderDate.Month == date.Month && taskForCurrentUser.Contains(f.Id)).ToList();
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

        /// <summary>
        /// Moves the value to the next
        /// </summary>
        private void Next()
        {
            if(Mode == CalendarMode.Month)
            {
                Date = Date.AddMonths(1);
            }
            else if(Mode == CalendarMode.Year)
            {
                Date = Date.AddYears(1);
            }
            else if (Mode == CalendarMode.Decade)
            {
                DecadeStart += 10;
                DecadeEnd += 10;
            }
            LoadData(Date);
        }

        /// <summary>
        /// Moves the value to the previous value
        /// </summary>
        private void Prev()
        {
            if (Mode == CalendarMode.Month)
            {
                Date = Date.AddMonths(-1);
            }
            else if (Mode == CalendarMode.Year)
            {
                Date = Date.AddYears(-1);
            }
            else if(Mode == CalendarMode.Decade)
            {
                DecadeStart -= 10;
                DecadeEnd -= 10;
            }
            LoadData(Date);
        }

        /// <summary>
        /// Fires when the mode is changed
        /// </summary>
        /// <param name="newMode"></param>
        private void ModeChanged(CalendarMode newMode)
        {
            Mode = newMode;
        }

        /// <summary>
        /// Indicates that a month is clicked
        /// </summary>
        private void MonthClicked(int month)
        {
            Mode = CalendarMode.Month;
            Date = new DateTime(Date.Year, month + 1, Date.Day);
            LoadData(Date);
        }

        /// <summary>
        /// Indicates that a year is clicked
        /// </summary>
        private void YearClicked(int year)
        {
            Mode = CalendarMode.Year;
            Date = new DateTime(year, Date.Month, Date.Day);
            LoadData(Date);
        }

        #endregion

    }
}
