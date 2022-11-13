using Company_Site.Base;

namespace Company_Site.Data
{
    public class CollectionSubEntry : BaseModel
    {

        public int CollectionEntryId { get; set; }

        public int BorrowerCode { get; set; }

        public string TrustCode { get; set; }

        public string TrustName { get; set; }

        public string HolderName { get; set; }

        public float Share { get; set; }

        public float Amount { get; set; }

    }
}
