using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

using System.ComponentModel;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class AssetDetails : ComponentBase
    {

        #region Private Properties

        /// <summary>
        /// The list of all the auction details enteries
        /// </summary>
        private List<AuctionDetails> AllAuctionDetailsEnteries { get; set; } = new List<AuctionDetails>();

        /// <summary>
        /// The list of all the valuation details enteries
        /// </summary>
        private List<ValuationDetails> AllValuationDetailEnteries { get; set; } = new List<ValuationDetails>();

        /// <summary>
        /// The list of asset details for the borrower
        /// </summary>
        private List<AssetDetailsModel> AssetDetailsEnteries { get; set; } = new List<AssetDetailsModel>();

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

        /// <summary>
        /// The record to remove
        /// </summary>
        private int RecordIdToRemove { get; set; } = -1;

        /// <summary>
        /// Handles when the delete confirmaiton should be shown
        /// </summary>
        private bool IsDeleteConfirmationVisible = false;

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
            ["FMV"] = p => p.FMV.ToString(),
            ["RSV"] = p => p.RSV.ToString(),
            ["DVS"] = p => p.DVS.ToString(),
            ["SCRAP"] = p => p.Share.ToString(),
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
            AssetDetailsEnteries = _dbContext.AssetDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            AllAuctionDetailsEnteries = _dbContext.AuctionDetails.ToList();
            AllValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
            Reset();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clears any new changes
        /// </summary>
        private void Reset()
        {
            NewEntry = new AssetDetailsModel();
            NewEntry.BorrowerCode = _applicationState.BorrowerCode;
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
            }
            else
                _dbContext.AssetDetails.Update(NewEntry);
            try
            {
                _dbContext.SaveChanges();
                Clear();
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

        /// <summary>
        /// Sets the table form to edit
        /// </summary>
        /// <param name="id"></param>
        private void Edit(int id)
        {
            NewEntry = _dbContext.AssetDetails.Where(f => f.Id == id).First();
            IsNewEntry = false;
            AuctionDetailEntries = _dbContext.AuctionDetails.Where(f => f.AssetDetailsId == id).ToList();
            ValuationDetailEnteries = _dbContext.ValuationDetails.Where(f => f.AssetDetailsId == id).ToList();
            StateHasChanged();
        }

        /// <summary>
        /// Clears and reset the table
        /// </summary>
        private void Clear()
        {
            AuctionDetailEntries.Clear();
            ValuationDetailEnteries.Clear();
            NewEntry = new AssetDetailsModel();
            NewEntry.BorrowerCode = _applicationState.BorrowerCode;
            AssetDetailsEnteries = _dbContext.AssetDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            AllValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
            AllAuctionDetailsEnteries = _dbContext.AuctionDetails.ToList();
            IsNewEntry = true;
            StateHasChanged();
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="id"></param>
        private void RemoveRecord(int id)
        {
            IsDeleteConfirmationVisible = true;
            RecordIdToRemove = id;
        }

        /// <summary>
        /// Deletes the record
        /// </summary>
        private void DeleteRecord()
        {
            if (RecordIdToRemove == -1)
                return;

            AssetDetailsModel details = _dbContext.AssetDetails.Where(f => f.Id == RecordIdToRemove).First();
            List<ValuationDetails> valuationDetails = _dbContext.ValuationDetails.Where(f => f.AssetDetailsId == details.Id).ToList(); 
            List<AuctionDetails> auctionDetails = _dbContext.AuctionDetails.Where(f => f.AssetDetailsId == details.Id).ToList();
            _dbContext.AuctionDetails.RemoveRange(auctionDetails);
            _dbContext.ValuationDetails.RemoveRange(valuationDetails);
            _dbContext.AssetDetails.Remove(details);
            _dbContext.SaveChanges();
            IsDeleteConfirmationVisible = false;
            AssetDetailsEnteries = _dbContext.AssetDetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            AllValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
            AllAuctionDetailsEnteries = _dbContext.AuctionDetails.ToList();
        }

        /// <summary>
        /// Cancels the confirmation notification
        /// </summary>
        private void CancelDeleteConfirmation()
        {
            IsDeleteConfirmationVisible = false;
            RecordIdToRemove = -1;
        }

        /// <summary>
        /// Returns the total FMV
        /// </summary>
        /// <returns></returns>
        private double TotalFMV()
        {
            List<int> ids = AssetDetailsEnteries.Select(f => f.Id).ToList();
            return AllValuationDetailEnteries.Where(f => ids.Contains(f.AssetDetailsId)).Sum(f => f.FMV);
        }

        /// <summary>
        /// Returns total RSV value
        /// </summary>
        /// <returns></returns>
        private double TotalRSV()
        {
            List<int> ids = AssetDetailsEnteries.Select(f => f.Id).ToList();
            return AllValuationDetailEnteries.Where(f => ids.Contains(f.AssetDetailsId)).Sum(f => f.RSV);
        }

        /// <summary>
        /// Returns total share value
        /// </summary>
        /// <returns></returns>
        private double TotalShare()
        {
            List<int> ids = AssetDetailsEnteries.Select(f => f.Id).ToList();
            return AllValuationDetailEnteries.Where(f => ids.Contains(f.AssetDetailsId)).Sum(f => f.Share);
        }

        #endregion

        #region Auction Details Methods

        private void AddAuctionDetails()
        {
            ShouldAddAuctionDetails = true;
            NewAuctionDetailsEntry = new AuctionDetails();
            NewAuctionDetailsEntry.AssetDetailsId = NewEntry.Id;
            AuctionDetailsFormShown = true;
        }

        private void SaveAuctionDetails()
        {
            _errors.Clear();
            if (IsNewEntry)
            {
                AuctionDetailsErrors.Add("Please insert auction details first otherwise inner enteries cannot be added.");
                return;
            }

            if (ShouldAddAuctionDetails)
                _dbContext.AuctionDetails.Add(NewAuctionDetailsEntry);
            else
                _dbContext.AuctionDetails.Update(NewAuctionDetailsEntry);
            _dbContext.SaveChanges();
            AuctionDetailEntries = _dbContext.AuctionDetails.Where(f => f.AssetDetailsId == NewEntry.Id).ToList();
            AllAuctionDetailsEnteries = _dbContext.AuctionDetails.ToList();
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
            NewAuctionDetailsEntry.AssetDetailsId = NewEntry.Id;
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
            AuctionDetailEntries = _dbContext.AuctionDetails.Where(f => f.AssetDetailsId == NewEntry.Id).ToList();
            AllAuctionDetailsEnteries = _dbContext.AuctionDetails.ToList();
            StateHasChanged();
            return AuctionDetailEntries;
        }

        #endregion

        #region Valuation Details Methods

        private void AddValuationDetails()
        {
            ShouldAddValuationDetails = true;
            NewValuationDetailsEntry = new ValuationDetails();
            NewValuationDetailsEntry.AssetDetailsId = NewEntry.Id;
            ValuationDetailsFormShown = true;
        }

        private void SaveValuationDetails()
        {
            _errors.Clear();
            if (IsNewEntry)
            {
                ValuationDetailsErrors.Add("Please insert Asset Details first");
                return;
            }

            if (ShouldAddValuationDetails)
                _dbContext.ValuationDetails.Add(NewValuationDetailsEntry);
            else
                _dbContext.ValuationDetails.Update(NewValuationDetailsEntry);
            _dbContext.SaveChanges();
            ValuationDetailEnteries = _dbContext.ValuationDetails.Where(f => f.AssetDetailsId == NewEntry.Id).ToList();
            AllValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
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
            NewValuationDetailsEntry.AssetDetailsId = NewEntry.Id;
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
            ValuationDetailEnteries = _dbContext.ValuationDetails.Where(f => f.AssetDetailsId == NewEntry.Id).ToList();
            AllValuationDetailEnteries = _dbContext.ValuationDetails.ToList();
            StateHasChanged();
            return ValuationDetailEnteries;
        }

        #endregion
    }
}
