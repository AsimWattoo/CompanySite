using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Radzen;

namespace Company_Site.Pages.User
{
    public partial class AccountManagement : BaseAddPage<TrustRelationViewModel, int>
    {

        #region Public Properties

        /// <summary>
        /// The user id of the logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The currently selected tab of the accounts page
        /// </summary>
        private AccountTypes CurrentType { get; set; } = AccountTypes.Home;

        private Dictionary<string, Func<TrustRelationViewModel, string>> Headers { get; set; } = new Dictionary<string, Func<TrustRelationViewModel, string>>()
        {
            ["Trust"] = t => t.Trust,
            ["Acquisition Date"] = t => t.AcquisitionDate.ToString("dd-MMM-yyyy"),
            ["Assignor Name"] = t => t.Assignor,
            ["ARC Shares"] = t => t.ARCShares.ToString(),
            ["Issuer Shares"] = t => Math.Round(t.IssuerShares, 2).ToString(),
            ["Total Sr Issued"] = t => Math.Round(t.TotalSrIssued, 2).ToString(),
            ["SR O/S"] = t => t.SR_O_S,
            ["Account Age"] = t => t.AccountAge.ToString(),
            ["NPA Date"] = t => t.NPADate.ToString("dd-MMM-yyyy"),
            ["POS"] = t => t.POS,
            ["TOS"] = t => t.TOS,
        };

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when it is initialized
        /// </summary>
        protected override void Setup()
        {
            
        }

        #endregion

        #region Private Methods

        private int GetId(TrustRelationViewModel model) => model.Id;

        protected override void LoadData()
        {
            List<TrustRelationModel> trustRelations = _dbContext.TrustRelations.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            Enteries = new List<TrustRelationViewModel>();
            foreach(TrustRelationModel model in trustRelations)
            {
                Enteries.Add(new TrustRelationViewModel()
                {
                    Id = model.Id,
                    AcquisitionDate = model.AcquistionDate,
                    Assignor = model.Assignor,
                    Trust = model.TrustCode,
                    TotalSrIssued = model.SRIssued,
                });
            }
        }

        /// <summary>
        /// Changes the display of the Accounts Page
        /// </summary>
        /// <param name="type"></param>
        private void _changePage(AccountTypes type)
        {
            CurrentType = type;
        }

        #endregion

    }
}
