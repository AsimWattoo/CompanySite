using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class NcltNetModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Case name is required")]
        public string case_name { get; set; }

        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Insolvery commencement date is required")]
        public DateTime? insolvery_commencement_date { get; set; }

        public string? IPE  { get; set; }

        [Required(ErrorMessage = "Applicant_name is required")]
        public string Applicant_name { get; set; }

        [Required(ErrorMessage = "Court is required")]
        public string court { get; set; }

        public string? PRA   { get; set; }

        public string? Asset_Details { get; set; }

        public string? IPR    { get; set; }

        public string? liquidator    { get; set; }

        public string? RA    { get; set; }

        public string? RP { get; set; }

        public double Liquidation_Value{ get; set; } 

        public string? current_stage { get; set; }

        public string? Plan { get; set; }

        public string? LiquidationDetails { get; set; }

        public string? CompaniesAct { get; set; }
    }
}
