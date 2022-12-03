using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class AccountDetailsModel : BaseModifierModel<int>
    {
        [Required(ErrorMessage = "Trust code is required")]
        public string TrustCode { get; set; }

        [Required(ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; }

        public string? Officers { get; set; }

        public string? GroupHead { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string Company_Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        public string? Plant_Location { get; set; }

        [Required(ErrorMessage = "Bank is required")]
        public string Bank { get; set; }

        public DateTime DateOfDefault { get; set; } = DateTime.Now;

        public string? ResolutionStatus { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        public string Industry { get; set; }

        public string? Products { get; set; }

        public string? Capacity { get; set; }

        public string? Branch { get; set; }

        public string? Last_Payment { get; set; }

        public string? ResolutionStrategy { get; set; }

        [Required(ErrorMessage = "Constitution is required")]
        public string Constitution { get; set; }

        [Required(ErrorMessage = "Operating Status is required")]
        public string Operating_Status { get; set; }

        [Required(ErrorMessage = "NPA Date is required")]
        public DateTime NPA_Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Acquisition date is required")]
        public DateTime Acquisition_Date { get; set; } = DateTime.Now;

        public string? Statutory_Dues { get; set; }

        public string? MeasuresOfReconstruction { get; set; }

        public string? DD_Official { get; set; }

        public string? DD_Firm { get; set; }

        public DateTime DD_Date { get; set; } = DateTime.Now;

        public DateTime FinalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Background for an account is required")]
        public string Background { get; set; }

        [Required(ErrorMessage = "Reason NPA is required")]
        public string Reason_NPA { get; set; }

        public string? Case_Exit { get; set; }

        public string? Status { get; set; }

        public DateTime Case_Exit_Date { get; set; } = DateTime.Now;
    }
}
