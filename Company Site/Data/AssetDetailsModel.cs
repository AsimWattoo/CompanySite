using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class AssetDetailsModel : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Charge is required")]
        public string? Charge { get; set; }


        [Required(ErrorMessage = "Asset Description is required")]
        public string? AssetDescription { get; set; }


        [Required(ErrorMessage = "Free Hold is required")]
        public string? FreeHold { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public string Owner { get; set; }


        [Required(ErrorMessage = "Mortgagor is required")]
        public string Mortgagor { get; set; }


        [Required(ErrorMessage = "Mortgage is required")]
        public string MortgageType { get; set; }


        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Possession is required")]
        public string? Possession { get; set; }


        public string? Address { get; set; }

        public string? LegalDescription { get; set; }  

        public string? Locality { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public long PinCode { get; set; }

        public string? East { get; set; }

        public string? West { get; set; }

        public string? North { get; set; }

        public string? South { get; set; }

        public DateTime LeasedDate { get; set; }=DateTime.Now;

        public string? Lessor { get; set; }

        public string? Lessee { get; set; }

        public string? Tenor { get; set; }
        
        [Required(ErrorMessage = "Area is required")]
        public string? Area { get; set; }

        [Required(ErrorMessage = "Rate is required")]
        public string? Rate { get; set; }

        public long InsuranceValue { get; set; }

        public long BookValue { get; set; }

        public DateTime NDC_Date { get; set; }=DateTime.Now;

        public string? Encumbrance { get; set; }

        public string? From { get; set; }

        public string? Custodian { get; set; }

        public string? SecurityTrustee { get; set; }

        public string? MortageDeed { get; set; }

        public string? DepositTitle { get; set; }

        public string? TitleDeed { get; set; }

        public string? Note { get; set; }

        public string? TypeOfSecurity { get; set; }

        public long RateOfHolding { get; set; }

        public string? ISIN { get; set; }

        public double HoldingPercentage { get; set; }

        public string? FaceValue { get; set; }

        public string? DP_ID { get; set; }

        public string? Listed { get; set; }

        public double CouponRate { get; set; }

        public string? DP_Name { get; set; }

        public string? Cersai_ID { get; set; }

        public string? Form { get; set; }
    }
}
