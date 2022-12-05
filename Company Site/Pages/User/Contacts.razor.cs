using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User
{
    public partial class Contacts
    {
        #region Contact Access Grants Properties

        public List<ContactAccessGrantModel> AccessGrants { get; set; } = new List<ContactAccessGrantModel>();

        public ContactAccessGrantModel NewAccessGrant { get; set; } = new ContactAccessGrantModel();

        public bool IsContactsAccessGrantFormShown { get; set; } = false;

        public bool ShouldAddContactsAccessGrant { get; set; } = true;

        public List<string> AccessGrantErrors { get; set; } = new List<string>();

        public Dictionary<string, Func<ContactAccessGrantModel, string>> GrantAccessHeaders { get; set; } = new Dictionary<string, Func<ContactAccessGrantModel, string>>()
        {
            ["Access Given By"] = p => p.AccessGivenBy,
            ["Access To"] = p => p.AccessTo,
            ["Start Date"] = p => p.StartDate.ToString("dd-MMM-yyyy"),
            ["End Date"] = p => p.EndDate.ToString("dd-MMM-yyyy"),
        };

        #endregion

        #region Contact Account Details Properties

        public List<ContactAccountDetailsModel> AccountDetails { get; set; } = new List<ContactAccountDetailsModel>();

        public ContactAccountDetailsModel NewAccountDetail{ get; set; } = new ContactAccountDetailsModel();

        public bool IsAccountDetailsFormShown { get; set; } = false;

        public bool ShouldAddAccountDetails{ get; set; } = true;

        public List<string> AccountDetailsErrors { get; set; } = new List<string>();

        public Dictionary<string, Func<ContactAccountDetailsModel, string>> AccountDetailsHeaders { get; set; } = new Dictionary<string, Func<ContactAccountDetailsModel, string>>()
        {
            ["Bank Name"] = p => p.BankName,
            ["Branch"] = p => p.Branch,
            ["Account Number"] = p => p.AccountNumber.ToString(),
            ["Account Type"] = p => p.AccountType,
            ["IFSCI Code"] = p => p.IFSCI_Code.ToString(),
            ["Additional"] = p => p.Additional,
        };

        #endregion

        #region Contact Payment History

        public List<ContactPaymentHistoryModel> PaymentHistories { get; set; } = new List<ContactPaymentHistoryModel>();

        public ContactPaymentHistoryModel NewPaymentHistory { get; set; } = new ContactPaymentHistoryModel();

        public bool IsPaymentHistoryFormShown { get; set; } = false;

        public bool ShouldAddPaymentHistory { get; set; } = true;

        public List<string> PaymentHistoryErrors { get; set; } = new List<string>();

        public Dictionary<string, Func<ContactPaymentHistoryModel, string>> PaymentHistoryHeaders { get; set; } = new Dictionary<string, Func<ContactPaymentHistoryModel, string>>()
        {
            ["Bill Number"] = p => p.BillNo.ToString(),
            ["Service"] = p => p.Service,
            ["Bill Amount"] = p => p.BillAmount.ToString(),
            ["Bill Date"] = p => p.BillDate.ToString("dd-MMM-yyyy"),
            ["Payment Date"] = p => p.PaymentDate.ToString("dd-MMM-yyyy"),
            ["Payment Amount"] = p => p.PaymentAmount.ToString(),
            ["Amount O/S"] = p => p.AmountO_S.ToString(),
        };

        #endregion

        #region Public Members

        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Protected Properties

        public List<ContactsModel> Enteries { get; set; } = new List<ContactsModel>();

        protected ContactsModel NewEntry { get; set; } = new ContactsModel();

        protected List<string> _errors = new List<string>();

        protected bool ShouldAdd { get; set; } = true;

        #endregion

        #region Injected Members

        [Inject]
        protected ApplicationDbContext _dbContext { get; set; }

        [Inject]
        protected ProtectedSessionStorage _storage { get; set; }

        [Inject]
        protected NavigationManager _navigationManager { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            Enteries = _dbContext.Contacts.ToList();
        }

        #endregion

        #region Protected Methods

        protected virtual void Save()
        {
            if (ShouldAdd)
                _dbContext.Contacts.Add(NewEntry);
            else
                _dbContext.Contacts.Update(NewEntry);
            _dbContext.SaveChanges();
            NewEntry = new ContactsModel();
            Enteries = _dbContext.Contacts.ToList();
            ShouldAdd = true;
            StateHasChanged();
        }

        public void EditRecord(string id)
        {
            ShouldAdd = false;
            NewEntry = _dbContext.Contacts.Where(t => t.ContactId.Equals(id)).First();
            AccessGrants = _dbContext.AccessGrants.Where(f => f.ContactId == NewEntry.ContactId).ToList();
            AccountDetails = _dbContext.ContactAccountDetails.Where(f => f.ContactId == NewEntry.ContactId).ToList();
            PaymentHistories = _dbContext.ContactPaymentHistories.Where(f => f.ContactId == NewEntry.ContactId).ToList();
            StateHasChanged();
        }

        protected void Clear()
        {
            NewEntry = new ContactsModel();
            ShouldAdd = true;
            StateHasChanged();
        }

        public void DeleteRecord(string id)
        {
            _dbContext.Contacts.Remove(_dbContext.Contacts.Where(f => f.ContactId == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbContext.Contacts.ToList();
            NewEntry = new ContactsModel();
            StateHasChanged();
        }

        #endregion

        #region Contacts Access Grants Methods

        public void AddAccessGrant()
        {
            ShouldAddContactsAccessGrant = true;
            IsContactsAccessGrantFormShown = true;
            NewAccessGrant = new ContactAccessGrantModel();
            NewAccessGrant.ContactId = NewEntry.ContactId;
        }

        public void EditAccessGrant(int id)
        {
            NewAccessGrant = _dbContext.AccessGrants.Where(f => f.Id == id).First();
            IsContactsAccessGrantFormShown = true;
            ShouldAddContactsAccessGrant = false;
            StateHasChanged();
        }

        public List<ContactAccessGrantModel> DeleteAccessGrant(int id)
        {
            ContactAccessGrantModel? accessGrant =  _dbContext.AccessGrants.Where(f => f.Id == id).FirstOrDefault();
            if(accessGrant != null)
            {
                _dbContext.AccessGrants.Remove(accessGrant);
                _dbContext.SaveChanges();
                AccessGrants = _dbContext.AccessGrants.ToList();
            }
            return AccessGrants;
        }

        public void ClearAccessGrant()
        {
            NewAccessGrant = new ContactAccessGrantModel();
        }

        public void SaveAccessGrant()
        {

            AccessGrantErrors.Clear();

            if(!_dbContext.Contacts.Any(f => f.ContactId == NewEntry.ContactId))
            {
                AccessGrantErrors.Add("Please add contact first.");
                return;
            }

            if (ShouldAddContactsAccessGrant)
                _dbContext.AccessGrants.Add(NewAccessGrant);
            else
                _dbContext.AccessGrants.Update(NewAccessGrant);
            try
            {
                _dbContext.SaveChanges();
                AccessGrants = _dbContext.AccessGrants.ToList();
                IsContactsAccessGrantFormShown = false;
            }
            catch (Exception ex)
            {
                AccessGrantErrors.Add(ex.Message);
            }
            StateHasChanged();
        }

        #endregion

        #region Contact Account Details Methods

        public void AddAccountDetails()
        {
            ShouldAddAccountDetails = true;
            IsAccountDetailsFormShown = true;
            NewAccountDetail = new ContactAccountDetailsModel();
            NewAccountDetail.ContactId = NewEntry.ContactId;
        }

        public void EditAccountDetails(int id)
        {
            NewAccountDetail = _dbContext.ContactAccountDetails.Where(f => f.Id == id).First();
            IsAccountDetailsFormShown = true;
            ShouldAddAccountDetails = false;
            StateHasChanged();
        }

        public List<ContactAccountDetailsModel> DeleteAccountDetails(int id)
        {
            ContactAccountDetailsModel? accountDetails = _dbContext.ContactAccountDetails.Where(f => f.Id == id).FirstOrDefault();
            if (accountDetails != null)
            {
                _dbContext.ContactAccountDetails.Remove(accountDetails);
                _dbContext.SaveChanges();
                AccountDetails = _dbContext.ContactAccountDetails.ToList();
            }
            return AccountDetails;
        }

        public void ClearAccountDetails()
        {
            NewAccountDetail = new ContactAccountDetailsModel();
        }

        public void SaveAccountDetail()
        {

            AccountDetailsErrors.Clear();

            if (!_dbContext.Contacts.Any(f => f.ContactId == NewEntry.ContactId))
            {
                AccountDetailsErrors.Add("Please add contact first.");
                return;
            }

            if (ShouldAddAccountDetails)
                _dbContext.ContactAccountDetails.Add(NewAccountDetail);
            else
                _dbContext.ContactAccountDetails.Update(NewAccountDetail);
            try
            {
                _dbContext.SaveChanges();
                AccountDetails = _dbContext.ContactAccountDetails.ToList();
                IsAccountDetailsFormShown = false;
            }
            catch (Exception ex)
            {
                AccountDetailsErrors.Add(ex.Message);
            }
            StateHasChanged();
        }

        #endregion

        #region Contact Payment History Methods

        public void AddPaymentHistory()
        {
            ShouldAddPaymentHistory = true;
            IsPaymentHistoryFormShown = true;
            NewPaymentHistory = new ContactPaymentHistoryModel();
            NewPaymentHistory.ContactId = NewEntry.ContactId;
        }

        public void EditPaymentHistory(int id)
        {
            NewPaymentHistory = _dbContext.ContactPaymentHistories.Where(f => f.Id == id).First();
            IsPaymentHistoryFormShown = true;
            ShouldAddPaymentHistory = false;
            StateHasChanged();
        }

        public List<ContactPaymentHistoryModel> DeletePaymentHistory(int id)
        {
            ContactPaymentHistoryModel? paymentHistory = _dbContext.ContactPaymentHistories.Where(f => f.Id == id).FirstOrDefault();
            if (paymentHistory != null)
            {
                _dbContext.ContactPaymentHistories.Remove(paymentHistory);
                _dbContext.SaveChanges();
                PaymentHistories = _dbContext.ContactPaymentHistories.ToList();
            }
            return PaymentHistories;
        }

        public void ClearPaymentHistory()
        {
            NewPaymentHistory = new ContactPaymentHistoryModel();
        }

        public void SavePaymentHistory()
        {

            PaymentHistoryErrors.Clear();

            if (!_dbContext.Contacts.Any(f => f.ContactId == NewEntry.ContactId))
            {
                PaymentHistoryErrors.Add("Please add contact first.");
                return;
            }

            if (ShouldAddPaymentHistory)
                _dbContext.ContactPaymentHistories.Add(NewPaymentHistory);
            else
                _dbContext.ContactPaymentHistories.Update(NewPaymentHistory);
            try
            {
                _dbContext.SaveChanges();
                PaymentHistories = _dbContext.ContactPaymentHistories.ToList();
                IsPaymentHistoryFormShown = false;
            }
            catch (Exception ex)
            {
                PaymentHistoryErrors.Add(ex.Message);
            }
            StateHasChanged();
        }

        public string GetTotal(string header)
        {
            switch (header)
            {
                case "Bill Amount":
                    return PaymentHistories.Sum(f => f.BillAmount).ToString();
                case "Payment Amount":
                    return PaymentHistories.Sum(f => f.PaymentAmount).ToString();
                case "Amount O/S":
                    return PaymentHistories.Sum(f => f.AmountO_S).ToString();
                default:
                    return "";
            }
        }

        #endregion
    }
}
