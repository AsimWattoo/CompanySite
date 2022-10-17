using Company_Site.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Company_Site.Areas.Identity.Pages.Shared
{
    public class PasswordLoginModel : PageModel
    {
        #region Public Properties

        [BindProperty]
        public PasswordModel PasswordModel { get; set; } = new PasswordModel();

        /// <summary>
        /// The url of the return page
        /// </summary>
        [BindProperty]
        public string _returnUrl { get; set; } = string.Empty;

        /// <summary>
        /// The Email received from the login page
        /// </summary>
        [BindProperty]
        public string _email { get; set; } = string.Empty;

        #endregion

        #region Private Members

        private UserManager<User> _userManager;

        private SignInManager<User> _signInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordLoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires when a get request for the page is generated
        /// </summary>
        /// <param name="email"></param>
        /// <param name="returnUrl"></param>
        public void OnGet(string email, string returnUrl)
        {
            this._returnUrl = returnUrl;
            this._email = email;
        }

        /// <summary>
        /// Fires when the form values are posted
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(_email);
                if(user != null)
                {
                    SignInResult res = await _signInManager.PasswordSignInAsync(user, this.PasswordModel.Password, true, false);
                    if (res.Succeeded)
                    {
                        return LocalRedirect(_returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("PasswordMisMatch", "Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserNotFound", "Email or password is incorrect");
                }
            }
            //Returning the current page if there are any errors
            return Page();
        }

        #endregion
    }

    public class PasswordModel
    {
        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
