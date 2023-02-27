using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ClaimsAndShareModel : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Lender Name is required")]
        public string LenderName { get; set; }

        public int ClaimSubmitted { get; set; }

        public int Admitted { get; set; }

        public double Share { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Voting Share is required")]
        public double VotingShare { get; set; }
    }
}
