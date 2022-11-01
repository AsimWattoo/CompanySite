using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class Trust
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TrustCode { get; set; }

        public string Trust_Name { get; set; }

        public int SRIssued { get; set; }

        public string SRIssuer { get; set; }

        public string SRHolder { get; set; }

        public string? Payable { get; set; }

        public string? TrusteeshipBasis { get; set; }

        public double TrusteeFee { get; set; }

        public int Face_Value { get; set; }

        public double IssuerShare { get; set; }

        public double HolderShare { get; set; }

        public string TrusteeshipLimit { get; set; }
        
        public string? ResolutionFeeBasis { get; set; }

        public DateTime TrustSetupDate { get; set; } = DateTime.Now;

        public string? NavBand { get; set; }

        public double IssuerUpsideShare { get; set; }

        public double HolderUpsideShare { get; set; }

        public string TrusteeshipCollection { get; set; }

        public double CollectionFee { get; set; }

        public string TrustDescription { get; set; }

        public string TermsAndConditions { get; set; }

        public string BeneficiaryName { get; set; }

        public string BranchNameAndAddress { get; set; }

        public string AccountNumber { get; set; }

        public string IFSCCode { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Modified { get; set; }

        public string ModifiedBy { get; set; }

        public string SROs { get; set; } = string.Empty;

        [NotMapped]
        public double Ratio => IssuerShare / HolderShare;

        public DateTime SetupDate { get; set; }

        public double ResolutionFee { get; set; }

        public string? InterestMode { get; set; }

        [NotMapped]
        public double TrustAge => (DateTime.Now - TrustSetupDate).TotalDays / 365;

        public double Year1 { get; set; }

        public double Year2 { get; set; }

        public double Year3 { get; set; }

        public double Year4 { get; set; }
        
        public double Year5 { get; set; }

        public double Year6 { get; set; }

        public double Year7 { get; set; }

        public double Year8 { get; set;}

        public double Year9 { get; set; }

        public string PanCard { get; set; }
    }
}
