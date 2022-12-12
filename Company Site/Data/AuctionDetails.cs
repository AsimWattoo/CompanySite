using Company_Site.Base;

namespace Company_Site.Data
{
    public class AuctionDetails : BaseModel<int>
    {
        public int AssetDetailsId { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public DateTime AuctionDate { get; set; } = DateTime.Now;

        public double ReservePrice { get; set; }

        public string? Outcome { get; set; }

        public int NumberOfBidder { get; set; }

        public double BuyPrice { get; set; }
    }
}
