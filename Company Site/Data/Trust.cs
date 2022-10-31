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

        public string Payable { get; set; }

        public string TrusteeshipBasis { get; set; }

        public string TrusteeFee { get; set; }

        public int Face_Value { get; set; }

        public double IssuerShare { get; set; }

        public double HolderShare { get; set; }

        public string TrusteeshipLimit { get; set; }
        
        public string ResolutionFeeBasis { get; set; }

        public DateTime TrustSetupDate { get; set; } = DateTime.Now;

        public string NavBand { get; set; }

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

    }
}
