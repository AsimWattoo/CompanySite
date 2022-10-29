
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class CollectionEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TrustCode { get; set; }

        [Required]
        public string Borrower { get; set; }

        [Required]
        public string Trust_Name { get; set; }

        [Required]
        public string BorrowerName { get; set; }

        public double CreditAmount { get; set; }

        public DateTime CreditDate { get; set; } = DateTime.Now;

        public string Source { get; set; }

        public string TypeOfRecovery { get; set; }

        public string? KYC { get; set; }

        public string NoneSeller { get; set; }

        public string NoneSellerShare { get; set; }

        public string? AdjustToward { get; set; }

        public bool Proportionately { get; set; }
    }
}
