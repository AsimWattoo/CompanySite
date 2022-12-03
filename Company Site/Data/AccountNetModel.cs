using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class AccountNetModel : BaseModel<string>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Case Title is required")]
        public string CaseTitle { get; set; }

        [Required(ErrorMessage = "Case Type is required")]
        public string? CaseType { get; set; }

        [Required(ErrorMessage = "Forum is required")]
        public string? Forum { get; set; }

        public string? JudgeName { get; set; }
        
        [Required(ErrorMessage = "Petitioner is required")]
        public string PetitionerMultiLine { get; set; }

        public string? Petitioner { get; set; }
        
        public string? Firm { get; set; }

        public string? ContactEmail { get; set; }
        
        public string? ContactMobile { get; set; }

        public double TotalFeesQuotation { get; set; }

        public double BilledAmount { get; set; }

        public double CourtFees { get; set; }

        public string? CaseDetails { get; set; }

        public string? Development { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string? FinalOrder { get; set; }
        
        [Required(ErrorMessage = "Court is required")]
        public string? Court { get; set; }

        public double ClaimAmount { get; set; }

        public string? Bench { get; set; }

        [Required(ErrorMessage = "Respondent is required")]
        public string? Respondent { get; set; }

        public string? Respondent_adv { get; set; }

        public double Advance { get; set; }
        
        public double AmountPaid { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Case Number is required")]
        public int CaseNumber { get; set; }

        public string? CNRNumber { get; set; }

        public int RegistrationNumber { get; set; }
        
        public DateTime NextDate { get; set; } = DateTime.Now;

        public DateTime FilingDate { get; set; } = DateTime.Now;

        public string? CurrentStage { get; set; }

        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        public double FeePerHearing { get; set; }

        public double OSAmount { get; set; }

        public double BillNumber { get; set; }

        public string? Status { get; set; }
    }
}
