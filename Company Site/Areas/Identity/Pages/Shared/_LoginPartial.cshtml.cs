using Company_Site.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Company_Site.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        #region Public Properties

        [BindProperty]
        public LoginInputModel Input { get; set; } = new LoginInputModel();

        [BindProperty]
        public string returnUrl { get; set; } = string.Empty;

        #endregion

        #region Private Members

        private UserManager<User> _userManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userManager"></param>
        public LoginModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fires when the page is retrieved
        /// </summary>
        /// <param name="returnUrl"></param>
        public void OnGet(string returnUrl)
        {
            if (returnUrl != null)
                this.returnUrl = returnUrl;
            else
                returnUrl = Url.Content("~/");
        }

        /// <summary>
        /// Checking whether the email is correct and if so then redirecting to the next page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(Input.Email);

                if(user == null)
                {
                    ModelState.AddModelError("Incorrect", "Email is incorrect");
                    return Page();
                }
                else
                {
                    Dictionary<string, string> parms = new Dictionary<string, string>()
                    {
                        ["email"] = this.Input.Email,
                        ["returnUrl"] = this.returnUrl
                    };
                    string url = QueryHelpers.AddQueryString("/password-login", parms);
                    return LocalRedirect(url);
                }

                //SignInResult res = await _signInManager.PasswordSignInAsync(user, Input.Password, true, false);

                //if (res.Succeeded)
                //{
                //    return LocalRedirect(returnUrl);
                //}
                //else
                //{
                //    ModelState.AddModelError("Incorrect", "Email or password is incorrect");
                //}
            }
            return Page();
        }

        #endregion
    }

    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
