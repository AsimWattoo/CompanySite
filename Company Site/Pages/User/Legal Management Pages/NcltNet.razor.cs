using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class NcltNet : BaseAddPage<NcltNetModel, int>
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

        private bool ClaimsAndShareFormShown { get; set; } = false;

        private bool ShouldAddClaimsAndShare { get; set; } = true;

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

        private bool TimelineFormShown { get; set; } = false;

        private bool ShouldAddTimeLine { get; set; } = true;

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.NcltNets;
        }

        protected override void LoadData()
        {
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("Please select a borrower from the account management page");
                return;
            }

            base.LoadData();
            NcltNetModel? entry = Enteries.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            //First time accessing this page
            if (entry == null)
            {
                entry = new NcltNetModel();
                entry.BorrowerCode = _applicationState.BorrowerCode;
                ShouldAdd = true;
            }
            else
            {
                ShouldAdd = false;
            }
            //Load NcltNet and its related data
            NewEntry = entry;
            ClaimsAndShareEnteries = _dbContext.ClaimsAndShares.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            RADetailsEnteries = _dbContext.RADetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            TimelineEnteries = _dbContext.Timelines.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        protected override void SaveResetup()
        {
        }

        #endregion

        #region Private Methods

        private void LiquidationValueChanged(string value)
        {
            if (!ShouldAdd)
            {
                NewEntry.LiquidationDetails = value;
                _dbContext.NcltNets.Update(NewEntry);
                _dbContext.SaveChanges();
            }
        }

        private void CompaniesActValueChanged(string value)
        {
            if (!ShouldAdd)
            {
                NewEntry.CompaniesAct = value;
                _dbContext.NcltNets.Update(NewEntry);
                _dbContext.SaveChanges();
            }
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
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddRADetails)
                _dbContext.RADetails.Add(NewRADetailsEntry);
            else
                _dbContext.RADetails.Update(NewRADetailsEntry);
            _dbContext.SaveChanges();
            RADetailsEnteries = _dbContext.RADetails.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            RADetailsFormShown = false;
            StateHasChanged();
        }

        private void EditRADetails(int id)
        {
            RADetailsFormShown = true;
            ShouldAddRADetails = false;
            NewRADetailsEntry = RADetailsEnteries.Where(f => f.Id == id).First();
            StateHasChanged();
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
            RADetailsEnteries = _dbContext.RADetails.ToList();
            StateHasChanged();
            return RADetailsEnteries;
        }

        #endregion

        #region Timeline Methods

        private void AddTimeline()
        {
            ShouldAddTimeLine = true;
            NewTimelineEntry = new TimelineModel();
            NewTimelineEntry.BorrowerCode = _applicationState.BorrowerCode;
            TimelineFormShown = true;
        }

        private void SaveTimeline()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddTimeLine)
                _dbContext.Timelines.Add(NewTimelineEntry);
            else
                _dbContext.Timelines.Update(NewTimelineEntry);
            _dbContext.SaveChanges();
            TimelineEnteries = _dbContext.Timelines.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            TimelineFormShown = false;
            StateHasChanged();
        }

        private void EditTimeline (int id)
        {
            TimelineFormShown = true;
            ShouldAddTimeLine = false;
            NewTimelineEntry = TimelineEnteries.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void ClearTimeline()
        {
            ShouldAddTimeLine = true;
            NewTimelineEntry = new TimelineModel();
            NewTimelineEntry.BorrowerCode = _applicationState.BorrowerCode;
        }

        private void CancelTimeline()
        {
            TimelineFormShown = false;
        }

        private List<TimelineModel> DeleteTimeline(int id)
        {
            TimelineModel model = _dbContext.Timelines.Where(f => f.Id == id).First();
            _dbContext.Timelines.Remove(model);
            _dbContext.SaveChanges();
            TimelineEnteries = _dbContext.Timelines.ToList();
            StateHasChanged();
            return TimelineEnteries;
        }

        #endregion

        #region Claims and Share Methods

        private void AddClaimsAndShare()
        {
            ShouldAddClaimsAndShare = true;
            NewClaimsAndSharesEntry = new ClaimsAndShareModel();
            NewClaimsAndSharesEntry.BorrowerCode = _applicationState.BorrowerCode;
            ClaimsAndShareFormShown = true;
        }

        private void SaveClaimsAndShare()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("No borrower is selected");
                return;
            }

            if (ShouldAddClaimsAndShare)
                _dbContext.ClaimsAndShares.Add(NewClaimsAndSharesEntry);
            else
                _dbContext.ClaimsAndShares.Update(NewClaimsAndSharesEntry);
            _dbContext.SaveChanges();
            ClaimsAndShareEnteries = _dbContext.ClaimsAndShares.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            ClaimsAndShareFormShown = false;
            StateHasChanged();
        }

        private void EditClaimsAndShare(int id)
        {
            ClaimsAndShareFormShown = true;
            ShouldAddClaimsAndShare = false;
            NewClaimsAndSharesEntry = ClaimsAndShareEnteries.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void ClearClaimsAndShare()
        {
            ShouldAddClaimsAndShare = true;
            NewClaimsAndSharesEntry = new ClaimsAndShareModel();
            NewClaimsAndSharesEntry.BorrowerCode = _applicationState.BorrowerCode;
        }

        private void CancelClaimsAndShare()
        {
            ClaimsAndShareFormShown = false;
        }

        private List<ClaimsAndShareModel> DeleteClaimsAndShare(int id)
        {
            ClaimsAndShareModel model = _dbContext.ClaimsAndShares.Where(f => f.Id == id).First();
            _dbContext.ClaimsAndShares.Remove(model);
            _dbContext.SaveChanges();
            ClaimsAndShareEnteries = _dbContext.ClaimsAndShares.ToList();
            StateHasChanged();
            return ClaimsAndShareEnteries;
        }

        private string GetClaimsAndShareTotal(string header)
        {
            switch (header)
            {
                case "Claim Submitted":
                    return ClaimsAndShareEnteries.Sum(f => f.ClaimSubmitted).ToString();
                case "Admitted":
                    return ClaimsAndShareEnteries.Sum(f => f.Admitted).ToString();
                case "Share":
                    return ClaimsAndShareEnteries.Sum(f => f.Share).ToString();
                case "Voting Share":
                    return ClaimsAndShareEnteries.Sum(f => f.VotingShare).ToString();
                default:
                    return "";
            }
        }

        #endregion
    }
}
