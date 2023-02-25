using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class InterestRateManager : BaseAddPage<InterestRateChangeModel, int>, ITable<InterestRateChangeModel, int>
    {
        #region Private Members

        public Dictionary<string, Func<InterestRateChangeModel, string>> Headers { get; set; } = new Dictionary<string, Func<InterestRateChangeModel, string>>()
        {
            ["Borrower"] = e => e.BorrowerCode.ToString(),
            ["Date"] = (InterestRateChangeModel e) => e.Date.ToString("dd-MMM-yyyy"),
            ["Revised Interest Rate"] = (InterestRateChangeModel e) => e.RevisedInterestRate.ToString(),
            ["Note"] = (InterestRateChangeModel e) => e.Note,
        };

        /// <summary>
        /// The list of accounts in the system
        /// </summary>
        private List<Account> Borrowers { get; set; } = new List<Account>();

        /// <summary>
        /// The list of debt profiles
        /// </summary>
        private List<DebtProfileModel> debtProfiles { get; set; } = new List<DebtProfileModel>();

        /// <summary>
        /// The value callback to provide value
        /// </summary>
        /// <param name="enteries"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private string ValueCallback(string header, InterestRateChangeModel entry)
        {
            switch (header)
            {
                case "Borrower":
                    return Borrowers
                        .Where(f => f.BorrowerCode == entry.BorrowerCode)
                        .First()
                        .Company;
                case "Date":
                case "Revised Interest Rate":
                case "Note":
                    return Headers[header](entry);
                default:
                    return "";
            }
        }

        /// <summary>
        /// The list of facilities for the borrower
        /// </summary>
        private List<string> _facilities = new List<string>();

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            Borrowers = _dbContext.Accounts.ToList();
            debtProfiles = _dbContext.DebtProfiles.ToList();
            _dbSet = _dbContext.InterestRateChangeEntries;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the id of the disbursement model
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(InterestRateChangeModel e) => e.Id;

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<InterestRateChangeModel> Search(List<InterestRateChangeModel> disbursmentModels, string text)
        {
            text = text.ToLower();
            return disbursmentModels.Where(e => e.BorrowerCode.ToString().Contains(text) || e.Date.ToString().Contains(text) || e.Note.Contains(text)).ToList();
        }

        /// <summary>
        /// Fires when borrower code changed
        /// </summary>
        /// <param name="code"></param>
        private void BorrowerCodeChanged(int code)
        {
            NewEntry.BorrowerCode = code;
            _facilities = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == code).Select(f => f.Facility).ToList();
			StateHasChanged();
        }

        /// <summary>
        /// Fires when the facility changes
        /// </summary>
        /// <param name="facility"></param>
        private void FacilityChanged(string facility)
        {
            NewEntry.Facility = facility;
        }

        #endregion
    }
}
