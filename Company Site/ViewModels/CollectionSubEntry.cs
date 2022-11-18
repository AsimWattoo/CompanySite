using Company_Site.Data;

namespace Company_Site.ViewModels
{
    public class CollectionSubEntryViewModel
    {

        #region Private Members

        private double share = 0;
        private double amount = 0;

        #endregion

        #region Public Properties

        public int Id { get; set; }

        public int CollectionId { get; set; }

        public int BorrowerCode { get; set; }

        public string BorrowerName { get; set; }

        public string TrustCode { get; set; }

        public string TrustName { get; set; }

        public string HolderName { get; set; }

        public double Share
        {
            get => Math.Round(share, 2);
            set => share = value;
        }

        public double Amount
        {
            get => Math.Round(amount, 2);
            set => amount = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CollectionSubEntryViewModel(int borrowerCode, string borrowerName, CollectionEntry entry, Trust t, double totalAmount)
        {
            if (!entry.CollectionId.HasValue)
                throw new ArgumentException("Collection Id cannot be null");
            CollectionId = entry.CollectionId.Value;
            Id = entry.Id;
            BorrowerCode = borrowerCode;
            BorrowerName = borrowerName;
            TrustCode = t.TrustCode;
            TrustName = t.Trust_Name;
            HolderName = t.SRHolder;
            Share = (entry.CreditAmount / totalAmount) * 100;
            Amount = entry.CreditAmount;
        }

        public CollectionSubEntryViewModel()
        {

        }

        #endregion

    }
}
