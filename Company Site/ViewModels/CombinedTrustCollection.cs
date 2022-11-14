namespace Company_Site.ViewModels
{
    public class CombinedTrustCollection
    {

        #region Private Members

        private double _totalCollection;

        #endregion

        public int Id { get; set; }

        public string Trust_Name { get; set; }

        public string TrustCode { get; set; }

        public double TotalCollection
        {
            get
            {
                return Math.Round(_totalCollection, 2);
            }

            set
            {
                _totalCollection = value;
            }
        }

    }
}
