using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Company_Site.Areas.Identity.Pages.Shared
{
    public class _LogoutPartialModel : PageModel
    {
        #region Private Members

        private SignInManager<User> _signInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public _LogoutPartialModel(SignInManager<User> singInManager)
        {
            _signInManager = singInManager;
        }

        #endregion

        public ActionResult OnGet()
        {
            //Signing out the user
            _signInManager.SignOutAsync();
            return LocalRedirect("/login?returnUrl=/");
        }
    }
}
