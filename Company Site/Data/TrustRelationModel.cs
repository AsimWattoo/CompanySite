using Company_Site.Base;
using Company_Site.DB;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class TrustRelationModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Trust code is required")]
        public string TrustCode { get; set; }

        public int BorrowerCode { get; set; }

        public DateTime AcquistionDate { get; set; } = DateTime.Now;

        public double AcquisitonPrice { get; set; }

        public int SRIssued { get; set; }

        [Required(ErrorMessage = "Assignor is required")]
        public string Assignor { get; set; }
    }
}
