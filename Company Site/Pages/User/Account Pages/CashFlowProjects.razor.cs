using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class CashFlowProjects : BaseAddPage<CashFlowProjectsModel>, ITable<CashFlowProjectsModel, int>
    {
        #region Public Properties

        public Dictionary<string, Func<CashFlowProjectsModel, string>> Headers { get; set; } = new Dictionary<string, Func<CashFlowProjectsModel, string>>()
        {
            ["Year"] = p => p.Year.ToString(),
            ["Apr-Jun"] = p => "0",
            ["Jul-Sept"] = p => "0",
            ["Oct-Dec"] = p => "0",
            ["Jan-Mar"] = p => "0",
            ["Total"] = p => "0",
        };

        private List<DebtProfileModel> debtProfiles = new List<DebtProfileModel>();

        #endregion

        #region Private Methods

        private string GetValue(string header, CashFlowProjectsModel cash)
        {
            string value = "";

            switch (header)
            {
                case "Apr-Jun":
                case "Jul-Sept":
                case "Oct-Dec":
                case "Jan-Mar":
                    List<CashFlowProjectsModel> model = _dbContext.CashFlows.Where(f => f.Year == cash.Year && f.quarter == header).ToList();
                    value = model.Sum(f => f.amount).ToString();
                    break;
                case "Total":
                    List<CashFlowProjectsModel> models= _dbContext.CashFlows.Where(f => f.Year == cash.Year).ToList();
                    value = models.Sum(f => f.amount).ToString();
                    break;
                case "Year":
                    value = cash.Year.ToString();
                    break;
                default:
                    value = "--Default--";
                    break;
            }

            return value;
        }

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.CashFlows;
            debtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        protected override void LoadData()
        {
            List<int> debtProfileIds = debtProfiles.Select(f => f.Id).ToList();
            Enteries = _dbSet.Where(f => debtProfileIds.Contains(f.Facilty))
                .AsEnumerable().DistinctBy(f => f.Year).ToList();
        }

        #endregion

        #region Public Methods

        public int GetId(CashFlowProjectsModel t) => t.Id;

        public bool SearchItem(CashFlowProjectsModel e)
        {
            return e.Year.ToString().Contains(_text) || e.FacilityName.Contains(_text) || e.quarter.Contains(_text) || e.source.Contains(_text);
        }

        private string _text;

        public List<CashFlowProjectsModel> Search(List<CashFlowProjectsModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        #endregion
    }
}
