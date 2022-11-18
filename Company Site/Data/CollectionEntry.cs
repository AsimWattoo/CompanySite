
using Company_Site.Base;
using Company_Site.Interfaces;

using MimeKit.Cryptography;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class CollectionEntry : BaseModel, ICloneable<CollectionEntry>
    {
        public int? CollectionId { get; set; } = null;

        public string? TrustCode { get; set; }

        [Required(ErrorMessage ="Borrower is required")]
        public int Borrower { get; set; }

        public string? Trust_Name { get; set; }

        [Required(ErrorMessage = "Borrower is required")]
        public string BorrowerName { get; set; }

        [Required(ErrorMessage ="Credit Amount is required")]
        public double CreditAmount { get; set; }

        [Required(ErrorMessage = "Credit date is required")]
        public DateTime CreditDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Type of recovery is required")]
        public string TypeOfRecovery { get; set; }

        public string? KYC { get; set; }

        public string? NoneSeller { get; set; }

        public double NoneSellerShare { get; set; }

        [Required(ErrorMessage =  "Adjust towards is required")]
        public string AdjustToward { get; set; }

        public bool Proportionately { get; set; }

        public int? Distribution_Id { get; set; } = null;

        public CollectionEntry Clone()
        {
            return new CollectionEntry()
            {
                Id = Id,
                CollectionId = CollectionId,
                TrustCode = TrustCode,
                Trust_Name = Trust_Name,
                Borrower = Borrower,
                BorrowerName = BorrowerName,
                AdjustToward = AdjustToward,
                CreditAmount = CreditAmount,
                CreditDate =CreditDate,
                Distribution_Id = Distribution_Id,
                KYC= KYC,
                NoneSeller = NoneSeller,
                NoneSellerShare= NoneSellerShare,
                Proportionately = Proportionately,
                Source = Source,
                TypeOfRecovery = TypeOfRecovery
            };
        }
    }
}
