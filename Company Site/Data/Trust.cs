using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class Trust : BaseModel
    {
        [Required(ErrorMessage = "Trust Code is required")]
        public string TrustCode { get; set; }

        [Required(ErrorMessage = "Trust Name is required")]
        public string Trust_Name { get; set; }

        [Required(ErrorMessage = "SR Issued is required")]
        public int SRIssued { get; set; }

        [Required(ErrorMessage = "SR Issuer is required")]
        public string SRIssuer { get; set; }

        [Required(ErrorMessage = "SR Holder is required")]
        public string SRHolder { get; set; }

        public string? Payable { get; set; }

        public string? TrusteeshipBasis { get; set; }

        public double TrusteeFee { get; set; }

        [Required(ErrorMessage = "Face Value is required")]
        public int Face_Value { get; set; }

        [Required(ErrorMessage = "Issuer Share is required")]
        public double IssuerShare { get; set; }

        [Required(ErrorMessage = "Holder Share is required")]
        public double HolderShare { get; set; }

        public string? TrusteeshipLimit { get; set; }
        
        public string? ResolutionFeeBasis { get; set; }

        [Required(ErrorMessage = "Trust Setup Date is required")]
        public DateTime TrustSetupDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Nav Band is required")]
        public string? NavBand { get; set; }

        [Required(ErrorMessage = "Issuer Upside Share is required")]
        public double IssuerUpsideShare { get; set; }

        [Required(ErrorMessage = "Holder Upside Share is required")]
        public double HolderUpsideShare { get; set; }

        public string? TrusteeshipCollection { get; set; }

        public double CollectionFee { get; set; }

        public string? TrustDescription { get; set; }

        public string? TermsAndConditions { get; set; }

        public string? BeneficiaryName { get; set; }

        public string? BranchNameAndAddress { get; set; }

        public string? AccountNumber { get; set; }

        public string? IFSCCode { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? Modified { get; set; }

        public string? ModifiedBy { get; set; }

        public string? SROs { get; set; } = string.Empty;

        [NotMapped]
        public double Ratio => Math.Round(IssuerShare / HolderShare, 2);

        public DateTime SetupDate { get; set; } = DateTime.Now;

        public double ResolutionFee { get; set; }

        public string? InterestMode { get; set; }

        [NotMapped]
        public double TrustAge => Math.Round((DateTime.Now - TrustSetupDate).TotalDays / 365, 2);

        public double T_Year1 { get; set; }

        public double T_Year2 { get; set; }

        public double T_Year3 { get; set; }

        public double T_Year4 { get; set; }
        
        public double T_Year5 { get; set; }

        public double T_Year6 { get; set; }

        public double T_Year7 { get; set; }

        public double T_Year8 { get; set;}

        public double T_Year9 { get; set; }

        public double R_Year1 { get; set; }

        public double R_Year2 { get; set; }

        public double R_Year3 { get; set; }

        public double R_Year4 { get; set; }

        public double R_Year5 { get; set; }

        public double R_Year6 { get; set; }

        public double R_Year7 { get; set; }

        public double R_Year8 { get; set; }

        public double R_Year9 { get; set; }

        public string? PanCard { get; set; }
    }
}
