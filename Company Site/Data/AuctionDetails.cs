using Company_Site.Base;

using Microsoft.Build.Framework;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class AuctionDetails : BaseModel<int>
    {
        public int AssetDetailsId { get; set; }

        public DateTime SaleIntimationNoticeDate { get; set; } = DateTime.Now;

        public DateTime PastingDate { get; set; } = DateTime.Now;

        public DateTime BidDocumentDate { get; set; } = DateTime.Now;

        public DateTime LetterOfConformationDate { get; set; } = new DateTime();

        public string? SuccessBidder { get; set; }

        public string? ContactNumber { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public DateTime AuctionDate { get; set; } = DateTime.Now;

        [Range(1, double.MaxValue, ErrorMessage = "Reserve Price is mandatory")]
        public double ReservePrice { get; set; }

        public string? Outcome { get; set; }

        public int NumberOfBidder { get; set; }

        public double BuyPrice { get; set; }
    }
}
