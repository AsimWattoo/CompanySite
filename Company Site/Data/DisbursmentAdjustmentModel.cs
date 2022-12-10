using Company_Site.Base;

namespace Company_Site.Data
{
    public class DisbursmentAdjustmentModel: BaseModel<int>
    {
        /// <summary>
        /// The borrower whose account is related to the adjustment
        /// </summary>
        public int BorrowerCode { get; set; }

        /// <summary>
        /// The account related to the adjustment
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// The id of the disbursment model
        /// </summary>
        public int DisbursmentId { get; set; }

        /// <summary>
        /// The amount of this adjustment
        /// </summary>
        public double Amount { get; set; }
    }
}
