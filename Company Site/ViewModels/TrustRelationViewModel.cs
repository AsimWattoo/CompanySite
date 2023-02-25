using Company_Site.Base;

namespace Company_Site.ViewModels
{
    public class TrustRelationViewModel : BaseModel<int>
    {
        public string Trust { get; set; }

        public DateTime AcquisitionDate { get; set; } = DateTime.Now;

        public string Assignor { get; set; }

        public double ARCShares { get; set; }

        public double IssuerShares { get; set; }

        public double TotalSrIssued { get; set; }

        public string SR_O_S { get; set; }

        public double AccountAge { get; set; }

        public DateTime NPADate { get; set; }

        public string POS { get; set; }

        public string TOS { get; set; }
    }
}
