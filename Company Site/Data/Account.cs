using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class Account : BaseModel
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
   }
}
