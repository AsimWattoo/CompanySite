using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Enum;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class LegalManagement : ComponentBase
    {
        #region Private Members

        private LegalTypes CurrentType = LegalTypes.CaseManagement;

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
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the current tab of the application
        /// </summary>
        /// <param name="legalTypes"></param>
        private void _changePage(LegalTypes legalTypes)
        {
            CurrentType = legalTypes;
        }

        #endregion
    }
}
