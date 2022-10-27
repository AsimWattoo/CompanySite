using Company_Site.Enum;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class Finance : ComponentBase
    {
        #region Private Members

        private FinanceTypes CurrentType { get; set; } = FinanceTypes.None;

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the display of the finance page
        /// </summary>
        /// <param name="fType"></param>
        private void _changePage(FinanceTypes fType)
        {
            if (fType == CurrentType)
                CurrentType = FinanceTypes.None;
            else
                CurrentType = fType;
        }

        #endregion
    }
}
