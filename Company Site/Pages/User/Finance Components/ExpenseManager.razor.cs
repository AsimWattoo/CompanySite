using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class ExpenseManager : BaseAddPage<ExpenseEntry>, ITable<ExpenseEntry, int>
    {
        #region Private Members

        public Dictionary<string, Func<ExpenseEntry, string>> Headers { get; set; } = new Dictionary<string, Func<ExpenseEntry, string>>()
        {
            ["Trust Code"] = (ExpenseEntry e) => e.TrustCode,
            ["Borrower Code"] = (ExpenseEntry e) => e.Borrower_Code.ToString(),
            ["Trust Name"] = (ExpenseEntry e) => e.Trust_Name,
            ["Vendor"] = (ExpenseEntry e) => e.Vendor_Name,
            ["Service"] = (ExpenseEntry e) => e.Service,
            ["Amount"] = (ExpenseEntry e) => e.BillAmount.ToString(),
            ["Payment Date"] = (ExpenseEntry e) => e.PaymentDate.ToString("dd-MMM-yyyy"),
        };

        private List<Trust> Trusts { get; set; } = new List<Trust>();

        private List<Account> Borrowers { get; set; } = new List<Account>();

        private List<Vendor> Vendors { get; set; } = new List<Vendor>();

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            Trusts = _dbContext.Trusts.ToList();
            Borrowers = _dbContext.Accounts.ToList();
            Vendors = _dbContext.Vendors.ToList();
            _dbSet = _dbContext.Expenses;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(ExpenseEntry e) => e.Id;

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<ExpenseEntry> Search(List<ExpenseEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Borrower_Code.Equals(text) || e.Trust_Name.Equals(text) || e.Service.Equals(text)).ToList();
        }

        private void TrustCodeChanged(string code)
        {
            NewEntry.TrustCode = code;
            NewEntry.Trust_Name = Trusts.Where(t => t.TrustCode == code).FirstOrDefault()?.Trust_Name;
        }

        private void BorrowerCodeChanged(int code)
        {
            NewEntry.Borrower_Code = code;
        }

        private void VendorChanged(int code)
        {
            NewEntry.Vendor_Id = code;
            NewEntry.Vendor_Name = _dbContext.Vendors.Where(v => v.Id == code).FirstOrDefault()?.VendorName;
        }

        #endregion
    }
}
