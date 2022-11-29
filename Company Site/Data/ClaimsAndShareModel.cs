using Company_Site.Base;

namespace Company_Site.Data
{
    public class ClaimsAndShareModel : BaseModel
    {
        public int BorrowerCode { get; set; }

        public string LenderName { get; set; }

        public int ClaimSubmitted { get; set; }

        public int Admitted { get; set; }

        public double Share { get; set; }

        public double VotingShare { get; set; }
    }
}
