using Company_Site.Base;
using Company_Site.Data;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class BorrowerDetailsAdd: BaseAddPage<BorrowerDetail>
    {

        #region Overriden Methods

        protected override void Setup()
        {
            PageModeKey = "BorrowerPageMode";
            IdKey = "BorrowerId";
            Records = () => _dbContext.BorrowerDetails.ToList();
            ReturnUrl = "/borrowerdetails";
            Save = save;
        }

        private bool save (BorrowerDetail t, bool addMode)
        {
            if (addMode)
                _dbContext.BorrowerDetails.Add(t);
            else
                _dbContext.BorrowerDetails.Update(t);
            return true;
        }

        #endregion
    }
}
