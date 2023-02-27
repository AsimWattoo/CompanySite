using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class RADetailsModel : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Plan Value is required")]
        public int PlanValue { get; set; }

        public int OurShare { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Financial Creditor Share is required")]
        public double FinancialCreditorShare { get; set; }

        [Required(ErrorMessage = "Payment Timeline is required")]
        public string PaymentTimeline { get; set; }

        public string? PlanDetails { get; set; }

        public double Scoring { get; set; }
    }
}
