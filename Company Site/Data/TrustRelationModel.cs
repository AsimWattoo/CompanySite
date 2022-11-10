using Company_Site.Base;

using Microsoft.AspNetCore.Routing.Constraints;

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

        /// <summary>
        /// The share for each trust
        /// </summary>
        public double Share { get; set; }

        /// <summary>
        /// The amount for each trust
        /// </summary>
        public double Amount { get; set; }
    }
}
