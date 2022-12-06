using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ValuationDetails : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Valuer is required")]
        public string Valuer { get; set; }

        public DateTime ValuationDate { get; set; } = DateTime.Now;

        public string? Property { get; set; }

        public string? FMV { get; set; }

        public string? RSV { get; set; }

        public string? DVS { get; set; }

        public string? Scrap { get; set; }

        public string? Note { get; set; }
    }
}
