using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DisburmentModel : BaseModel<int>
    {
        public DateTime Date { get; set; } = DateTime.Now;
        
        [Range(1, double.MaxValue, ErrorMessage = "Amount is required")]
        public double Amount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; } = -1;

        public string? Note { get; set; }
        public string? AdjustTowards { get; set; }
    }
}
