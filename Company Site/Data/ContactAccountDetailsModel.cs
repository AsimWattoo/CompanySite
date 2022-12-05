using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ContactAccountDetailsModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Contact Id is required")]
        public string? ContactId { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public int AccountNumber { get; set; }

        public string AccountType { get; set; }

        public string IFSCI_Code { get; set; }

        public string? Additional { get; set; }
    }
}
