using Company_Site.Base;

namespace Company_Site.Data
{
    public class DisburmentModel : BaseModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public double Amount { get; set; }
        public int BorrowerCode { get; set; }
        public string? Note { get; set; }
        public string? AdjustTowards { get; set; }
    }
}
