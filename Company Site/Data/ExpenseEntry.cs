using Microsoft.Build.Framework;

using Key = System.ComponentModel.DataAnnotations.KeyAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using Company_Site.Base;

namespace Company_Site.Data
{
    public class ExpenseEntry : BaseModel
    {

        [Required]
        public string TrustCode { get; set; }

        [Required]
        public int Borrower_Code { get; set; }

        [Required]
        public string Trust_Name { get; set; }

        public int? Distribution_Id { get; set; } = null;

        public string? Service { get; set; }

        public double BillAmount { get; set; }

        public int Vendor_Id { get; set; }
        
        public string? Vendor_Name { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string? PaymentMode { get; set; }

        public double PaymentAmount { get; set; }

        public bool Proportionately { get; set; } = false;
    }
}
