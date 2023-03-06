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

        public double Opening { get; set; }

        public double Collections { get; set; }

        public double TSFee { get; set; }

        public double RFees { get; set; }

        public double CFee { get; set; }

        public double OtherExpense { get; set; }

        public double Adjustment { get; set; }

        public double Balance { get; set; }

        public double TDS_Decimal { get; set; }

        public double Advance_TDS_Decimal { get; set; }

        public double Distribution_AMT { get; set; }

        public double NormalDist { get; set; }

        public double Surplus { get; set; }

        public double Provision { get; set; }

        public double ClosingBalance { get; set; }

        public double OPFV { get; set; }

        public double CLFV { get; set; }

        public double FaceValue { get; set; }
    }
}
