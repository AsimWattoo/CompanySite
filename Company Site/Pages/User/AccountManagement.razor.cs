using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using Radzen;

namespace Company_Site.Pages.User
{
    public partial class AccountManagement : BaseAddPage<TrustRelationModel, int>
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

        private Dictionary<string, Func<TrustRelationModel, string>> Headers { get; set; } = new Dictionary<string, Func<TrustRelationModel, string>>()
        {
            ["Trust"] = t => t.Trust.Trust_Name,
            ["Total SR Issued"] = t => t.Trust.SRIssued.ToString(),
        };

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when it is initialized
        /// </summary>
        protected override void Setup()
        {
            _dbSet = _dbContext.TrustRelations;
        }

        #endregion

        #region Private Methods

        private int GetId(TrustRelationModel model) => model.Id;

        protected override void LoadData()
        {
            Enteries = _dbContext.TrustRelations.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            Enteries.ForEach(e =>
            {
                e.Account = _dbContext.Accounts.Where(a => a.BorrowerCode == e.BorrowerCode).FirstOrDefault();
                e.Trust = _dbContext.Trusts.Where(t => t.TrustCode == e.TrustCode).FirstOrDefault();
            });
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
