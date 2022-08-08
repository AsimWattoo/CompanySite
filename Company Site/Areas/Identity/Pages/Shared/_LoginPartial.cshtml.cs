using Company_Site.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Company_Site.Areas.Identity.Pages
{
    public class LoginModel : PageModel
    {
        #region Public Properties

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [BindProperty]
        public string returnUrl { get; set; }

        #endregion

        #region Private Members

        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        #endregion

        #region Constructor

        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Public Methods

        public void OnGet(string returnUrl)
        {
            if (returnUrl != null)
                this.returnUrl = returnUrl;
            else
                returnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(Input.Email);

                if(user == null)
                {
                    ModelState.AddModelError("Incorrect", "Username or password is incorrect");
                    return Page();
                }

                SignInResult res = await _signInManager.PasswordSignInAsync(user, Input.Password, true, false);

                if (res.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("Incorrect", "Username of password is incorrect");
                }
            }
            return Page();
        }

        #endregion
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
