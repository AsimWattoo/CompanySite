using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class LegalManagement : ComponentBase
    {
        #region Private Members

        private Dictionary<string, Func<AccountNetModel, string>> Headers { get; set; } = new Dictionary<string, Func<AccountNetModel, string>>()
        {
            ["CaseTitle"] = p => p.CaseTitle,
            ["Forum"] = p => p.Forum,
            ["Status"] = p => p.Status,
            ["Next Date"] = p => p.NextDate.ToString("dd-MMM-yyyy"),
            ["Order/Outcome"] = p => p.FinalOrder,
        };

        private List<AccountNetModel> _accountNets = new List<AccountNetModel>();

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
            _accountNets = _dbContext.AccountNets.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the id of the account net model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetId(AccountNetModel model) => model.Id;

        #endregion
    }
}
