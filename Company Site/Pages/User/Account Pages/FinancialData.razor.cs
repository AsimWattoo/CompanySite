using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class FinancialData : ComponentBase
    {

        #region Private Members

        private List<int> Years = new List<int>();

        /// <summary>
        /// The dictionary of financial data enteries
        /// </summary>
        private Dictionary<int, FinancialDataModel> _financialDataEnteries { get; set; } = new Dictionary<int, FinancialDataModel>();

        /// <summary>
        /// The new financial data entry
        /// </summary>
        private FinancialDataModel NewEntry { get; set; } = new FinancialDataModel();

        /// <summary>
        /// The list of financial enteries
        /// </summary>
        private List<FinancialDataModel> FinancialEnteries { get; set; } = new List<FinancialDataModel>();

        /// <summary>
        /// The list of errors for the page
        /// </summary>
        private List<string> _errors = new List<string>();

        /// <summary>
        /// Indicates the current mode of the page
        /// </summary>
        private bool ShouldAdd = true;

        /// <summary>
        /// Indicates whether the delete confirmation is shown or not
        /// </summary>
        private bool IsDeleteConfirmationShown = false;

        /// <summary>
        /// The year which is selected for edit
        /// </summary>
        private int SelectedYear = -1;

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ApplicationState _applicationState { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {

            if(_applicationState.BorrowerCode == -1)
            {
                _errors.Add("Please select an account from the account management page.");
                return;
            }
            Clear();
            //Loading Data for the borrower
            LoadData();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the data in the requried variables
        /// </summary>
        private void LoadData()
        {
            FinancialEnteries = _dbContext.FinancialDatas.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).OrderBy(f => f.Year).ToList();
            Years = FinancialEnteries.Select(f => f.Year).Distinct().ToList();
            _financialDataEnteries = FinancialEnteries.ToDictionary(f => f.Year, f => f);
        }

        /// <summary>
        /// Saves the new entry to the database
        /// </summary>
        private void Save()
        {
            _errors.Clear();
            //Checking if the valid year is selected or not
            if(NewEntry.Year == -1)
            {
                _errors.Add("Please select an year");
                return;
            }

            //Adding the new entry
            if (ShouldAdd)
            {
                //Finding an existing entry for the same borrower and year
                FinancialDataModel? model = _dbContext.FinancialDatas.Where(f => f.BorrowerCode == _applicationState.BorrowerCode && f.Year == NewEntry.Year).FirstOrDefault();
                if (model != null)
                {
                    _errors.Add("An entry for the specified year already exists. Please edit or remove that");
                    return;
                }
                _dbContext.FinancialDatas.Add(NewEntry);
            }
            else
            {
                //Finding an existing entry for the same borrower and year
                FinancialDataModel? model = _dbContext.FinancialDatas.Where(f => f.BorrowerCode == _applicationState.BorrowerCode && f.Year == NewEntry.Year).FirstOrDefault();
                if (model != null && SelectedYear != model.Year)
                {
                    _errors.Add("An entry for the specified year already exists. Please edit or remove that");
                    return;
                }
                _dbContext.FinancialDatas.Update(NewEntry);
            }
            try
            {
                _dbContext.SaveChanges();
                Clear();
                LoadData();
            }
            catch(Exception exc)
            {
                _errors.Add(exc.Message.ToString());
            }
        }

        /// <summary>
        /// Loads the entry for editing
        /// </summary>
        private void Edit(int year)
        {
            FinancialDataModel? model = FinancialEnteries.Where(f => f.Year == year).FirstOrDefault();
            if(model != null)
            {
                ShouldAdd = false;
                SelectedYear = year;
                NewEntry = model;
            }
        }

        /// <summary>
        /// Deletes the selected entry
        /// </summary>
        /// <param name="year"></param>
        private void Delete()
        {
            if (!Years.Contains(NewEntry.Year))
                return;

            //Getting the model to delete
            FinancialDataModel model = _dbContext.FinancialDatas.Where(f => f.Year == NewEntry.Year && f.BorrowerCode == _applicationState.BorrowerCode).First();
            _dbContext.FinancialDatas.Remove(model);
            _dbContext.SaveChanges();
            IsDeleteConfirmationShown = false;
            Clear();
            LoadData();
        }

        /// <summary>
        /// Clears the form and resets it
        /// </summary>
        public void Clear()
        {
            _errors.Clear();
            ShouldAdd = true;
            NewEntry = new FinancialDataModel();
            NewEntry.BorrowerCode = _applicationState.BorrowerCode;
        }
        
        /// <summary>
        /// Initiates the deletion of the entry
        /// </summary>
        private void DeleteRecord()
        {
            IsDeleteConfirmationShown = true;
        }

        /// <summary>
        /// Cancels the delete confirmation form
        /// </summary>
        private void CancelDeleteConfirmation()
        {
            IsDeleteConfirmationShown = false;
        }

        #endregion

    }
}
