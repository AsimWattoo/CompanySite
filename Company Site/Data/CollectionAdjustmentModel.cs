using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class CollectionAdjustmentModel : BaseModel<int>
    {

        /// <summary>
        /// The borrower for whom the collection adjustment has been done
        /// </summary>
        public int BorrowerCode { get; set; }

        /// <summary>
        /// The code of the trust this entry is linked to
        /// </summary>
        [Required(ErrorMessage = "Trust code is required")]
        public string? TrustCode { get; set; }

        /// <summary>
        /// The id common for all the collections of a single record
        /// </summary>
        public int? CombinedCollectionId { get; set; }

        /// <summary>
        /// The individual collection id
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// The number of the account for whom this entry is created
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// The amount for collection
        /// </summary>
        public double Amount { get; set; }
    }
}
