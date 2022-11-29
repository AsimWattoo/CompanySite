using Company_Site.Base;

namespace Company_Site.Data
{
    public class RADetailsModel : BaseModel
    {
        public int BorrowerCode { get; set; }

        public string? Name { get; set; }

        public int PlanValue { get; set; }

        public int OurShare { get; set; }

        public string? PaymentTimeline { get; set; }

        public string? PlanDetails { get; set; }

        public double Scoring { get; set; }
    }
}
