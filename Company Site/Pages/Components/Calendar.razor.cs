using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class Calendar : ComponentBase
    {

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

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the control is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            int numberOfDays = DateTime.DaysInMonth(2023, 2);
            for(int i = 1; i <= numberOfDays; i++)
            {
                DateTime date = new DateTime(2023, 2, i);
                Days.Add((date.ToString("ddd"), i));
            }
        }

        #endregion

    }
}
