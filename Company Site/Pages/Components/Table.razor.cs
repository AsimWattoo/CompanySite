using Company_Site.Data;
using Company_Site.Enum;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Internal;

using System.Reflection;

namespace Company_Site.Pages.Components
{
    public partial class Table<T, T2> : ComponentBase
        where T: new()
    {

        #region Parameters

        [Parameter]
        public Func<T, T2> GetId { get; set; }

        [Parameter]
        public bool ShowTotal { get; set; } = false;

        [Parameter]
        public Func<string, string> GetTotal { get; set; }

        /// <summary>
        /// Fires when a row is clicked
        /// </summary>
        [Parameter]
        public Action<T> OnRowClick { get; set; }

        /// <summary>
        /// Indicates whether to show the paging buttons or not
        /// </summary>
        [Parameter]
        public bool ShowPaging { get; set; } = true;

        /// <summary>
        /// Indicates whether the table is searchable or not
        /// </summary>
        [Parameter]
        public bool IsSearchable { get; set; } = true;

        [Parameter]
        //public Dictionary<string, Func<List<T>, Sorting, IEnumerable<T>>> Headers { get; set; } = new Dictionary<string, Func<List<T>, Sorting, IEnumerable<T>>>();
        public Dictionary<string, Func<T, string>> Headers { get; set; } = new Dictionary<string, Func<T, string>>();

        [Parameter]
        public Func<string, T, string>? ValueCallback { get; set; }

        [Parameter]
        public List<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                UpdateRows();
            }
        }

        [Parameter]
        public Func<T2, List<T>> OnDelete { get; set; }

        [Parameter]
        public Action<T2> OnEdit { get; set; }

        [Parameter]
        public Func<List<T>, string, List<T>> OnSearch { get; set; }

        [Parameter]
        public Func<T, bool> IsEditable { get; set; } = (T t) => true;

        [Parameter]
        public Func<T, bool> IsDeleteable { get; set; } = (T t) => true;

        [Parameter]
        public bool ShowSerialNumber { get; set; } = false;

        [Parameter]
        public Func<T, bool> IsViewable { get; set; } = (T t) => false;

        [Parameter]
        public Action<T2> ViewItem { get; set; }

        /// <summary>
        /// Tells whether a field will act as input or not
        /// </summary>
        [Parameter]
        public Dictionary<string, TableInput<T>?> InputFields { get; set; } = new Dictionary<string, TableInput<T>?>();

        /// <summary>
        /// Fires when the editable field value is saved
        /// </summary>
        [Parameter]
        public Action<T> OnSave { get; set; }

        /// <summary>
        /// Tells whether the table is editable or not
        /// </summary>
        [Parameter]
		public bool IsSaveable { get; set; }

        /// <summary>
        /// Indicates to show confirmation message for deletion
        /// </summary>
        [Parameter]
        public bool ShowDeleteConfirmation { get; set; } = true;

        [Parameter]
        public string? ViewHeader { get; set; }

        #endregion

        #region Private Members

        private int SR = 1;

        private List<T> _items = new List<T>();

        //The copy of the items to be kept while searching
        private List<T> itemsCopy { get; set; }

        //The model to be used for searching
        private SearchBarModel SearchModel { get; set; } = new SearchBarModel();

        ///the number of pages to show
        private int _NumberOfPages { get; set; } = 1;

        private List<int> Pages
        {
            get
            {
                {
                    List<int> pages = new List<int>();
                    for (int p = 1; p <= _NumberOfPages; p++)
                        pages.Add(p);
                    return pages;
                }
            }
        }

        private int _CurrentPage { get; set; } = 1;

        private List<T> DisplayedItems = new List<T>();

        private T2 userIdToDelete;

        //The text of the search box
        private string SearchText;

        private Dictionary<string, Sorting> Sortings = new Dictionary<string, Sorting>();

        private bool IsDeleteConfirmationVisible { get; set; } = false;

        #endregion

        #region Overriden Methods

        protected override void OnAfterRender(bool firstRender)
        {
            SR = 1;
        }

        #endregion

        #region Private Methods

        private void SetInputValue(string header, ChangeEventArgs e, T record)
        {
            Type type = typeof(T);
            if(InputFields[header]?.PropertyName != null)
            {
                PropertyInfo property = type.GetProperty(InputFields[header].PropertyName);
                try
                {
                    property?.SetValue(record, Convert.ChangeType(e.Value, property.PropertyType));
                }
                catch
                {
                    property?.SetValue(record, InputFields[header].DefaultValue);
                }
            }
        }

        private bool CheckInput(string header)
        {
            bool value = InputFields.ContainsKey(header) && InputFields[header] != null;
            return value;
        }

        /// <summary>
        /// Returns list of table rows
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
		private string GetHeaderValue(string header, T record)
		{
            return Headers[header].Invoke(record);
		}

		/// <summary>
		/// Sorts the records
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private List<T> SortRecords(string name)
        {
            return (Sortings[name] == Sorting.Ascending
                ? DisplayedItems.OrderBy(f => Headers[name](f)).ToList()
                : (Sortings[name] == Sorting.Descending
                    ? DisplayedItems.OrderByDescending(f => Headers[name](f)).ToList()
                    : DisplayedItems));
        }

        private void UpdateInitialization()
        {
            SR = 1;
            _NumberOfPages = (Items.Count / SearchModel.RowsToShow) + 1;
            DisplayedItems = Items.Take(SearchModel.RowsToShow * _CurrentPage).ToList();
            _CurrentPage = 1;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            UpdateInitialization();
            //Copying items for later recovery
            itemsCopy = Items;
            foreach (string header in Headers.Keys)
            {

                if (string.IsNullOrEmpty(header))
                    continue;

                Sortings.Add(header, Sorting.None);
            }

        }

        //Fires when selected rows to show changes
        private void RowsToShowChange(ChangeEventArgs e)
        {
            SearchModel.RowsToShow = int.Parse(e.Value.ToString());
            UpdateRows();
        }

        private void ChangePage(int page)
        {
            _CurrentPage = page;
            UpdateRows();
        }

        private void PreviousPage()
        {
            if (_CurrentPage == 1)
                return;

            _CurrentPage = _CurrentPage - 1;
            UpdateRows();
        }

        private void NextPage()
        {
            if (_CurrentPage == _NumberOfPages)
                return;

            _CurrentPage = _CurrentPage + 1;
            UpdateRows();
        }

        private void UpdateRows()
        {
            SR = 1;
            if (ShowPaging)
            {
                DisplayedItems = Items.Take(_CurrentPage * SearchModel.RowsToShow).Skip((_CurrentPage - 1) * SearchModel.RowsToShow).ToList();
            }
            else
            {
                DisplayedItems = Items;
            }
            string sortingColumn = Sortings.Where(s => s.Value != Sorting.None).FirstOrDefault().Key;
            if (sortingColumn != null)
            {
                DisplayedItems = SortRecords(sortingColumn);
            }
            SR = 1;
        }

        private void ResetSorting(string toSkip = null)
        {
            foreach (string key in Sortings.Keys)
            {
                if (key == toSkip)
                    continue;
                Sortings[key] = Sorting.None;
            }
        }

        private void DeleteRecordId(T2 id)
        {
            userIdToDelete = id;
            if (!ShowDeleteConfirmation)
            {
                DeleteRecord();
			}
            else
				IsDeleteConfirmationVisible = true;
		}

		private void CancelDeleteConfirmation()
        {
            IsDeleteConfirmationVisible = false;
        }

        ///Sorts the table with the specified column
        private void Sort(string name)
        {
            SR = 1;
            ResetSorting(name);
            if (Sortings[name] == Sorting.None)
                Sortings[name] = Sorting.Ascending;
            else if (Sortings[name] == Sorting.Ascending)
                Sortings[name] = Sorting.Descending;
            else
                Sortings[name] = Sorting.Ascending;

            //DisplayedItems = OnSortRecords(DisplayedItems, name, Sortings[name]);
            DisplayedItems = SortRecords(name);
        }


        //Deletes the record
        private void DeleteRecord()
        {
            if (OnDelete != null)
            {
                Items = OnDelete(userIdToDelete);
            }
            IsDeleteConfirmationVisible = false;
        }

        //Edits the record
        private void EditRecord(T2 id)
        {
            if (OnEdit != null)
            {
                OnEdit(id);
            }
        }

        //Searches the records
        private void SearchRecords()
        {
            if (!string.IsNullOrEmpty(SearchText) && !string.IsNullOrWhiteSpace(SearchText))
            {
                Items = OnSearch(Items, SearchText);
            }
            else
            {
                Items = itemsCopy;
            }
            UpdateInitialization();
        }

        private void ClearSearch()
        {
			Items = itemsCopy;
            SearchText = "";
            UpdateInitialization();
		}

        private void ViewRecord(T2 id)
        {
            if (ViewItem != null)
            {
                ViewItem(id);
            }
        }

        #endregion

    }
}
