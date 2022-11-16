using Company_Site.Data;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Trust code is required")]
        public string TrustCode { get; set; }

        [Required(ErrorMessage = "Assignor is required")]
        public string Assignor { get; set; }

        [Required(ErrorMessage = "Trust name is required")]
        public string TrustName { get; set; }

        [Required(ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string Company { get; set; }

        public DateTime AcquistionDate { get; set; } = DateTime.Now;

        public double AcquisitonPrice { get; set; }

        public int SRIssued { get; set; }

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccountViewModel()
        {

        }

        public AccountViewModel(Account ac, Trust t)
        {
            TrustCode = t.TrustCode;
            TrustName = t.Trust_Name;
            Assignor = ac.Assignor;
            BorrowerCode = ac.BorrowerCode;
            Company = ac.Company;
            AcquisitonPrice = ac.AcquisitonPrice;
            AcquistionDate = ac.AcquistionDate;
            SRIssued = ac.SRIssued;
        }

        #endregion
    }
}
