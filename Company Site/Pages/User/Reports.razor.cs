using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Radzen;

using System.Reflection;

namespace Company_Site.Pages.User
{
    public partial class Reports : ComponentBase
    {

        #region Private Members

        /// <summary>
        /// The headers for the table to be shown
        /// </summary>
        private dynamic Headers { get; set; } = new Dictionary<string, Func<dynamic, string>>();

        /// <summary>
        /// Returns the id of the table
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private dynamic GetId(dynamic v) => v.Id;

        /// <summary>
        /// The list of the enteries to be shown in the table
        /// </summary>
        private dynamic Enteries = new List<object>();

        /// <summary>
        /// The currently selected table
        /// </summary>
        private string? SelectedTable;

        /// <summary>
        /// The list of tables to be shown
        /// </summary>
        private Dictionary<string, Type> Tables = new Dictionary<string, Type>();

        #endregion

        #region Injected Members

        [Inject]
        public ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the page is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            Tables = new Dictionary<string, Type>()
            {
                ["Trust Master"] = typeof(Trust),
                ["Borrower Details"] = typeof(BorrowerDetail)
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires when the currently selected table changes
        /// </summary>
        /// <param name="table"></param>
        private void TableChanged(string table)
        {
            //List of all tables
            Type t = Tables[table];
            PropertyInfo[] properties = t.GetProperties();
            Dictionary<string, Func<dynamic, string>> headers= new Dictionary<string, Func<dynamic, string>>();
            foreach(PropertyInfo property in properties)
            {
                headers.Add(property.Name, t =>
                {
                    dynamic value = property.GetValue(t);
                    if (value != null)
                    {
						if (property.PropertyType == typeof(DateTime))
							return value.ToString("dd-MMM-yyyy");

						else
							return value.ToString();
					}
					else
						return "";

				});
                Headers = headers;
            }
            PropertyInfo prop = typeof(ApplicationDbContext).GetProperties().Where(f =>
            {
                if (f.PropertyType.GenericTypeArguments[0] == t)
                {
                    return true;
                }
                else
                    return false;
            })
                .First();
            var method = typeof(Enumerable).GetMethod("ToList");
            var generic = method?.MakeGenericMethod(t);
            dynamic result = generic.Invoke(prop, new object[] { prop.GetValue(_dbContext) });
            Enteries = result;
            StateHasChanged();
        }

        #endregion

    }
}
