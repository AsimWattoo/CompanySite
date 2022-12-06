using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class AssetDetails : ComponentBase
    {

        #region Private Properties

        /// <summary>
        /// The entry for the asset details
        /// </summary>
        private AssetDetailsModel NewEntry { get; set; }

        /// <summary>
        /// Indicates whether this is the new entry being created for the borrower
        /// </summary>
        private bool IsNewEntry { get; set; } = true;

        /// <summary>
        /// The list of errors which have occurred while processing
        /// </summary>
        private List<string> _errors { get; set; } = new List<string>();

        #endregion

        #region Auction Details Properties

        public Dictionary<string, Func<AuctionDetails, string>> AuctionDetailsHeaders { get; set; } = new Dictionary<string, Func<AuctionDetails, string>>()
        {
            ["Publication Date"] = p => p.PublicationDate.ToString("dd-MMM-yyyy"),
            ["Auction Date"] = p => p.AuctionDate.ToString("dd-MMM-yyyy"),
            ["Reserve Price"] = p => p.ReservePrice.ToString(),
            ["Outcome"] = p => p.Outcome,
            ["Number of Bidders"] = p => p.NumberOfBidder.ToString(),
            ["Buy Price"] = p => p.BuyPrice.ToString(),

        };

        public int GetAuctionId(AuctionDetails t) => t.Id;

        public List<AuctionDetails> AuctionDetailEntries { get; set; } = new List<AuctionDetails>();

        public AuctionDetails NewAuctionDetailsEntry { get; set; } = new AuctionDetails();

        private bool AuctionDetailsFormShown { get; set; } = false;

        private bool ShouldAddAuctionDetails { get; set; } = true;

        private List<string> AuctionDetailsErrors { get; set; } = new List<string>();

        #endregion

        #region Valuation Details Properties

        public Dictionary<string, Func<ValuationDetails, string>> ValuationDetailsHeaders { get; set; } = new Dictionary<string, Func<ValuationDetails, string>>()
        {
            ["Valuer"] = p => p.Valuer,
            ["Valuation Date"] = p => p.ValuationDate.ToString("dd-MMM-yyyy"),
            ["Property"] = p => p.Property,
            ["FMV"] = p => p.FMV,
            ["RSV"] = p => p.RSV,
            ["DVS"] = p => p.DVS,
            ["SCRAP"] = p => p.Scrap,
            ["Note"] = p => p.Note,

        };

        public int GetValuationId(ValuationDetails t) => t.Id;

        public List<ValuationDetails> ValuationDetailEnteries { get; set; } = new List<ValuationDetails>();

        public ValuationDetails NewValuationDetailsEntry { get; set; } = new ValuationDetails();

        private bool ValuationDetailsFormShown { get; set; } = false;

        private bool ShouldAddValuationDetails { get; set; } = true;

        private List<string> ValuationDetailsErrors { get; set; } = new List<string>();

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        [Inject]
        private ApplicationState _applicationState { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            AssetDetailsModel? model = _dbContext.AssetDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            AuctionDetailEntries = _dbContext.AuctionDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            ValuationDetailEnteries = _dbContext.ValuationDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();

            if(model == null)
            {
                NewEntry = new AssetDetailsModel();
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
            }
            else
            {
                NewEntry = model;
                IsNewEntry = false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clears any new changes
        /// </summary>
        private void Reset()
        {
            if (IsNewEntry)
            {
                NewEntry = new AssetDetailsModel();
                NewEntry.BorrowerCode = _applicationState.BorrowerCode;
            }
            else
                NewEntry = _dbContext.AssetDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).First();
            StateHasChanged();
        }

        /// <summary>
        /// Saves the Asset Details Entry
        /// </summary>
        private void Save()
        {
            _errors.Clear();
            if(_applicationState.BorrowerCode < 0)
            {
                _errors.Add("No Account Selected. Please select account from the account management page");
                return;
            }

            //Adding if this is the new entry
            if (IsNewEntry)
            {
                _dbContext.AssetDetails.Add(NewEntry);
                IsNewEntry = false;
            }
            else
                _dbContext.AssetDetails.Update(NewEntry);
            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    _errors.Add(ex.InnerException.Message);
                else
                    _errors.Add(ex.Message);
            }
            StateHasChanged();
        }

        #endregion

        #region Auction Details Methods

        private void AddAuctionDetails()
        {
            ShouldAddAuctionDetails = true;
            NewAuctionDetailsEntry = new AuctionDetails();
            NewAuctionDetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
            AuctionDetailsFormShown = true;
        }

        private void SaveAuctionDetails()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                AuctionDetailsErrors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddAuctionDetails)
                _dbContext.AuctionDetails.Add(NewAuctionDetailsEntry);
            else
                _dbContext.AuctionDetails.Update(NewAuctionDetailsEntry);
            _dbContext.SaveChanges();
            AuctionDetailEntries = _dbContext.AuctionDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            AuctionDetailsFormShown = false;
            StateHasChanged();
        }

        private void EditAuctionDetails(int id)
        {
            AuctionDetailsFormShown = true;
            ShouldAddAuctionDetails = false;
            NewAuctionDetailsEntry = AuctionDetailEntries.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void ClearAuctionDetails()
        {
            ShouldAddAuctionDetails = true;
            NewAuctionDetailsEntry = new AuctionDetails();
            NewAuctionDetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
        }

        private void CancelAuctionDetails()
        {
            AuctionDetailsFormShown = false;
        }

        private List<AuctionDetails> DeleteAuctionDetails(int id)
        {
            AuctionDetails model = _dbContext.AuctionDetails.Where(f => f.Id == id).First();
            _dbContext.AuctionDetails.Remove(model);
            _dbContext.SaveChanges();
            AuctionDetailEntries = _dbContext.AuctionDetails.ToList();
            StateHasChanged();
            return AuctionDetailEntries;
        }

        #endregion

        #region Valuation Details Methods

        private void AddValuationDetails()
        {
            ShouldAddValuationDetails = true;
            NewValuationDetailsEntry = new ValuationDetails();
            NewValuationDetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
            ValuationDetailsFormShown = true;
        }

        private void SaveValuationDetails()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                ValuationDetailsErrors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddValuationDetails)
                _dbContext.ValuationDetails.Add(NewValuationDetailsEntry);
            else
                _dbContext.ValuationDetails.Update(NewValuationDetailsEntry);
            _dbContext.SaveChanges();
            ValuationDetailEnteries = _dbContext.ValuationDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            ValuationDetailsFormShown = false;
            StateHasChanged();
        }

        private void EditValuationDetails (int id)
        {
            ValuationDetailsFormShown = true;
            ShouldAddValuationDetails = false;
            NewValuationDetailsEntry = ValuationDetailEnteries.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void ClearValuationDetailsDetails()
        {
            ShouldAddValuationDetails = true;
            NewValuationDetailsEntry = new ValuationDetails();
            NewValuationDetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
        }

        private void CancelValuationDetails()
        {
            ValuationDetailsFormShown = false;
        }

        private List<ValuationDetails> DeleteValuationDetails(int id)
        {
            ValuationDetails model = _dbContext.ValuationDetails.Where(f => f.Id == id).First();
            _dbContext.ValuationDetails.Remove(model);
            _dbContext.SaveChanges();
            ValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
            StateHasChanged();
            return ValuationDetailEnteries;
        }

        #endregion
    }
}
