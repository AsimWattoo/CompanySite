using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ValuationDetails : BaseModel<int>
    {
        public int AssetDetailsId { get; set; }

        [Required(ErrorMessage = "Valuer is required")]
        public string Valuer { get; set; }

        [Required(ErrorMessage = "Valudation Date is required")]
        public DateTime? ValuationDate { get; set; }

        public string? Property { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "FMV is required")]
        public double FMV { get; set; }

        public double RSV { get; set; }

        public double DVS { get; set; }

        public double Share{ get; set; }

        public string? Note { get; set; }
    }
}
