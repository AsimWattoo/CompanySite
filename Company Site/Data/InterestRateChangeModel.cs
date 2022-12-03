using Company_Site.Base;

using System.Security.Principal;

namespace Company_Site.Data
{
	public class InterestRateChangeModel : BaseModel<int>
	{
		public DateTime Date { get; set; } = DateTime.Now;
		public double RevisedInterestRate { get; set; }
		public int BorrowerCode { get; set; }
		public string? Note { get; set; }
	}
}
