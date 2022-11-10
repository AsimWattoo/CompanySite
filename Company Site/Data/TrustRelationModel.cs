using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class TrustRelationModel : BaseModel
    {
        [NotMapped]
        public Trust? Trust { get; set; }

        [Required(ErrorMessage = "Trust code is required")]
        public string TrustCode { get; set; }

        public int AccountId { get; set; }
        [NotMapped]
        public Account? Account { get; set; }
    }
}
