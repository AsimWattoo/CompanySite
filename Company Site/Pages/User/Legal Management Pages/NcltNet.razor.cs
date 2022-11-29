using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class ncltnet : BaseAddPage<NcltNetModel>
    {
        #region Claims and Shares Properties

        public Dictionary<string, Func<ClaimsAndShareModel, string>> ClaimsAndShareHeaders { get; set; } = new Dictionary<string, Func<ClaimsAndShareModel, string>>()
        {
            ["Lender Name"] = p => p.LenderName,
            ["Claim Submitted"] = p => p.ClaimSubmitted.ToString(),
            ["Admitted"] = p => p.Admitted.ToString(),
            ["Share"] = p => p.Share.ToString(),
            ["Voting Share"] = p => p.VotingShare.ToString(),
            
        };

        public int GetClaimsAndShareId(ClaimsAndShareModel t) => t.Id;

        public List<ClaimsAndShareModel> ClaimsAndShareEnteries { get; set; } = new List<ClaimsAndShareModel>();

        public ClaimsAndShareModel NewClaimsAndSharesEntry { get; set; } = new ClaimsAndShareModel();

        #endregion

        #region RA Details Properties

        public Dictionary<string, Func<RADetailsModel, string>> RADetailsHeaders { get; set; } = new Dictionary<string, Func<RADetailsModel, string>>()
        {
            ["Name"] = p => p.Name,
            ["Plan Value"] = p => p.PlanValue.ToString(),
            ["Our Share"] = p => p.OurShare.ToString(),
            ["Payment Timeline"] = p => p.PaymentTimeline,
            ["Plan Details"] = p => p.PlanDetails,
            ["Scoring"] = p => p.Scoring.ToString(),
        };

        public int GetRADetailsId(RADetailsModel t) => t.Id;

        public List<RADetailsModel> RADetailsEnteries { get; set; } = new List<RADetailsModel>();

        public RADetailsModel NewRADetailsEntry { get; set; } = new RADetailsModel();

        private bool RADetailsFormShown { get; set; } = false;

        private bool ShouldAddRADetails { get; set; } = true;

        #endregion

        #region Timeline Table Properties

        public Dictionary<string, Func<TimelineModel, string>> TimelineHeaders { get; set; } = new Dictionary<string, Func<TimelineModel, string>>()
        {
            ["Date"] = p => p.Date.ToString("dd-MMM-yyyy"),
            ["Particulars"] = p => p.Particulars,
        };

        public int GetTimeLineId(TimelineModel t) => t.Id;

        public List<TimelineModel> TimelineEnteries { get; set; } = new List<TimelineModel>();

        public TimelineModel NewTimelineEntry { get; set; } = new TimelineModel();

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.NcltNets;
        }

        protected override void LoadData()
        {
            if(_applicationState.BorrowerCode < 0)
            {
                _errors.Add("Please select a borrower from the account management page");
                return;
            }

            base.LoadData();
            NcltNetModel? entry = Enteries.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            //First time accessing this page
            if(entry == null)
            {
                entry = new NcltNetModel();
                entry.BorrowerCode = _applicationState.BorrowerCode;
            }
            //Load NcltNet and its related data
            NewEntry = entry;
            ClaimsAndShareEnteries = _dbContext.ClaimsAndShares.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            RADetailsEnteries = _dbContext.RADetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            TimelineEnteries = _dbContext.Timelines.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        #endregion

        #region RA Details Methods

        private void AddRADetials()
        {
            ShouldAddRADetails = true;
            NewRADetailsEntry = new RADetailsModel();
            NewRADetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
            RADetailsFormShown = true;
        }

        private void SaveRADetails()
        {
            _errors.Clear();
            if(_applicationState.BorrowerCode < 0)
            {
                _errors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddRADetails)
                _dbContext.RADetails.Add(NewRADetailsEntry);
            else
                _dbContext.RADetails.Update(NewRADetailsEntry);
            _dbContext.SaveChanges();
            StateHasChanged();
            RADetailsEnteries = _dbContext.RADetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        private void EditRADetails(int id)
        {
            ShouldAddRADetails = false;
            NewRADetailsEntry = RADetailsEnteries.Where(f => f.Id == id).First();
            RADetailsFormShown = true;
        }

        private void ClearRADetails()
        {
            ShouldAddRADetails = true;
            NewRADetailsEntry = new RADetailsModel();
            NewRADetailsEntry.BorrowerCode = _applicationState.BorrowerCode;
        }

        private void CancelRADetails()
        {
            RADetailsFormShown = false;
        }

        private List<RADetailsModel> DeleteRADetails(int id)
        {
            RADetailsModel model = _dbContext.RADetails.Where(f => f.Id == id).First();
            _dbContext.RADetails.Remove(model);
            _dbContext.SaveChanges();
            RADetailsEnteries = _dbContext.RADetails();
            StateHasChanged();
            return RADetailsEnteries;
        }

        #endregion
    }
}
