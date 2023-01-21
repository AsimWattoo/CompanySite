using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class DisbursementManager : BaseAddPage<DisburmentModel, int>, ITable<DisburmentModel, int>
    {
        #region Private Members

        public Dictionary<string, Func<DisburmentModel, string>> Headers { get; set; } = new Dictionary<string, Func<DisburmentModel, string>>()
        {
            ["Date"] = (DisburmentModel e) => e.Date.ToString("dd-MMM-yyyy"),
            ["Amount"] = (DisburmentModel e) => e.Amount.ToString(),
            ["Note"] = (DisburmentModel e) => e.Note,
            ["Adjust Towards"] = (DisburmentModel e) => e.AdjustTowards,
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
        private string ValueCallback(string header, DisburmentModel entry)
        {
            switch (header)
            {
                case "Borrower":
                    return Borrowers
                        .Where(f => f.BorrowerCode == entry.BorrowerCode)
                        .First()
                        .Company;
                case "Date":
                case "Amount":
                case "Note":
                case "Adjust Towards":
                    return Headers[header](entry);
                default:
                    return "";
            }
        }

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            Borrowers = _dbContext.Accounts.ToList();
            debtProfiles = _dbContext.DebtProfiles.ToList();
            _dbSet = _dbContext.Disburments;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the id of the disbursement model
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(DisburmentModel e) => e.Id;

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<DisburmentModel> Search(List<DisburmentModel> disbursmentModels, string text)
        {
            text = text.ToLower();
            return disbursmentModels.Where(e => e.AdjustTowards.Contains(text) || e.BorrowerCode.ToString().Contains(text) || e.Date.ToString().Contains(text) || e.Note.Contains(text)).ToList();
        }

        private void BorrowerCodeChanged(int code)
        {
            NewEntry.BorrowerCode = code;
            StateHasChanged();
        }

        #endregion
    }
}
