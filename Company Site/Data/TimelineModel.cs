using Company_Site.Base;

namespace Company_Site.Data
{
    public class TimelineModel : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string? Particulars { get; set; }
    }
}
