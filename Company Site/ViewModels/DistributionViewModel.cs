using Company_Site.Data;

namespace Company_Site.ViewModels
{
    public class DistributionViewModel
    {

        public string TrustCode { get; set; }

        public string TrustName { get; set;}

        public string TrustSetupDate { get; set; }

        public double TrustAge { get; set; }

        public Trust Trust { get; set; } = new Trust();
    }
}
