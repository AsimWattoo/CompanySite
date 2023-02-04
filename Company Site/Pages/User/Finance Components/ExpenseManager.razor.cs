using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

using Microsoft.AspNetCore.Components;

using System.Data.Common;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class ExpenseManager : BaseAddPage<ExpenseEntry, int>, ITable<ExpenseEntry, int>
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

        private List<DebtProfileModel> DebtProfiles = new List<DebtProfileModel>();

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

        public override void OnEdit(int id)
        {
            DebtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == NewEntry.Borrower_Code).ToList();
        }

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
            DebtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == NewEntry.Borrower_Code).ToList();
            StateHasChanged();
        }

        private void OnVendorChanged(object vendor)
        {
            if(vendor is int vendorId) 
            {
                NewEntry.Vendor_Id = vendorId;
                Vendor v = _dbContext.Vendors.Where(f => f.Id == vendorId).First();
                NewEntry.Vendor_Name = v.VendorName;
                NewEntry.GSTNumber = v.GSTNumber;
            }
        }

        protected override void OnDelete(ExpenseEntry item)
        {
            List<ExpenseAdjustmentModel> adjustments = _dbContext.ExpenseAdjustments.Where(f => f.ExpenseId == item.Id).ToList();
            _dbContext.ExpenseAdjustments.RemoveRange(adjustments);
        }

        protected override void AfterSave()
        {
            if (!ShouldAdd)
            {
                List<ExpenseAdjustmentModel> expenseAdjustments = _dbContext.ExpenseAdjustments.Where(f => f.ExpenseId == NewEntry.Id).ToList();
                _dbContext.ExpenseAdjustments.RemoveRange(expenseAdjustments);
                _dbContext.SaveChanges();
            }

            if (NewEntry.AdjustTowards == "prop")
            {
                double totalPos = DebtProfiles.Sum(f => f.POS);
                DebtProfiles.ForEach(f =>
                {
                    double proportion = f.POS / totalPos;
                    double amount = proportion * NewEntry.BillAmount;
                    _dbContext.ExpenseAdjustments.Add(new ExpenseAdjustmentModel()
                    {
                        AccountNumber = f.AccountNumber,
                        ExpenseId = NewEntry.Id,
                        Amount = amount,
                        BorrowerCode = NewEntry.Borrower_Code,
                    });
                });
            }
            else
            {
                _dbContext.ExpenseAdjustments.Add(new ExpenseAdjustmentModel()
                {
                    AccountNumber = int.Parse(NewEntry.AdjustTowards),
                    Amount = NewEntry.BillAmount,
                    ExpenseId = NewEntry.Id,
                    BorrowerCode = NewEntry.Borrower_Code
                });
            }

            _dbContext.SaveChanges();
        }

        private void VendorChanged(int code)
        {
            NewEntry.Vendor_Id = code;
            NewEntry.Vendor_Name = _dbContext.Vendors.Where(v => v.Id == code).FirstOrDefault()?.VendorName;
        }

        #endregion
    }
}
