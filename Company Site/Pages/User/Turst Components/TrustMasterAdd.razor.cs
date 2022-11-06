using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using TestFramework.Enums;
using TestFramework.Extensions;
using TestFramework.Models;

namespace Company_Site.Pages.User.Turst_Components
{
	public partial class TrustMasterAdd : BaseAddPage<Trust>
	{

        #region Overridden Methods

        protected override void Setup()
        {
            ReturnUrl = "/trustmaster";
            IdKey = "TrustId";
            PageModeKey = "TrustPageMode";
            Records = () => _dbContext.Trusts.ToList();
            Save = (t, addMode) =>
            {
                TestResult res = NewEntry.ToTestObject(true)
                    .NumberRange(t => t.IssuerShare, 1d, double.MaxValue, "Issuer share should be greater than 0")
                    .NumberRange(t => t.IssuerUpsideShare, 1d, double.MaxValue, "Issuer upside shares should be greater than 0")
                    .NumberRange(t => t.HolderShare, 1d, double.MaxValue, "Holder shares should be greater than 0")
                    .NumberRange(t => t.HolderUpsideShare, 1d, double.MaxValue, "Holder Upside Shares should be greater than 0")
                    .Execute();
                if (res.TestStatus == Status.Falied)
                {
                    _errors = res.Errors.ToList();
                    return false;
                }

                if (addMode)
                    _dbContext.Trusts.Add(NewEntry);
                else
                    _dbContext.Trusts.Update(NewEntry);
                return true;
            };
        }

        #endregion

    }
}
