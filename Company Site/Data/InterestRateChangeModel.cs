using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Company_Site.Data
{
	public class InterestRateChangeModel : BaseModel<int>
	{
		public DateTime Date { get; set; } = DateTime.Now;
		public double RevisedInterestRate => BaseInterestRate + Spread;

		[Range(0, int.MaxValue, ErrorMessage = "Borrower Code is required")]
		public int BorrowerCode { get; set; } = -1;
		public string? Note { get; set; }

		[Range(1, double.MaxValue, ErrorMessage = "Base Interest Rate is required")]
		public double BaseInterestRate { get; set; }

		public double Spread { get; set; }

		[Required(ErrorMessage = "Facility is required")]
		public string Facility { get; set; }
	}
}
