using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DistributionEntry
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Trust code is required")]
        public string Trust_Code { get; set; }

        [Required(ErrorMessage = "Trust name is required")]
        public string Trust_Name { get; set; }

        public int SRIssued { get; set; }

        public int SROutStanding { get; set; }

        public float Opening { get; set; }

        public float Collections { get; set; }

        public float TSFee { get; set; }

        public float RFees { get; set; }

        public float CFee { get; set; }

        public float OtherExpense { get; set; }

        public float Adjustment { get; set; }

        public float Balance { get; set; }

        public float TDS_Decimal { get; set; }

        public float Advance_TDS_Decimal { get; set; }

        public float Distribution_AMT { get; set; }

        public float NormalDist { get; set; }

        public float Surplus { get; set; }

        public float Provision { get; set; }

        public float ClosingBalance { get; set; }

        public float OPFV { get; set; }

        public float CLFV { get; set; }
    }
}
