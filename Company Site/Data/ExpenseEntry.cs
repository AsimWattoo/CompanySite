using Microsoft.Build.Framework;

using Key = System.ComponentModel.DataAnnotations.KeyAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class ExpenseEntry
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TrustCode { get; set; }

        [Required]
        public string Borrower_Code { get; set; }

        [Required]
        public string Trust_Name { get; set; }

        public string Service { get; set; }

        public double Amount { get; set; }

        public int Vendor_Id { get; set; }

        [ForeignKey("Id")]
        public Vendor Vendor { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
