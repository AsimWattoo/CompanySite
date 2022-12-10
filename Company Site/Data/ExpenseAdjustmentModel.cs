using Company_Site.Base;

namespace Company_Site.Data
{
    public class ExpenseAdjustmentModel : BaseModel<int>
    {
        /// <summary>
        /// The borrower code towards which the adjustment is being made
        /// </summary>
        public int BorrowerCode { get; set; }

        /// <summary>
        /// The id of the account for which this adjustment is being done
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// The expense Id from which adjustment amount is obtained
        /// </summary>
        public int ExpenseId { get; set; }

        /// <summary>
        /// The amount of the adjustment
        /// </summary>
        public double Amount { get; set; }
    }
}
