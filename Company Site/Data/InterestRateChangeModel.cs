using Company_Site.Base;

using System.Security.Principal;

namespace Company_Site.Data
{
	public class InterestRateChangeModel : BaseModel<int>
	{
		public DateTime Date { get; set; } = DateTime.Now;
		public double RevisedInterestRate => BaseInterestRate + Spread;
		public int BorrowerCode { get; set; }
		public string? Note { get; set; }
		public double BaseInterestRate { get; set; }
		public double Spread { get; set; }
		public string? Facility { get; set; }
	}
}
