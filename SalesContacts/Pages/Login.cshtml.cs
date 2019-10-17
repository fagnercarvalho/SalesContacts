using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesContacts.Data.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesContacts.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private SignInManager<UserSys> signInManager;

        public LoginModel(SignInManager<UserSys> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string LoginError { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await this.signInManager.PasswordSignInAsync(this.Email, this.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", "The e-mail and/ or password entered is invalid. Please try again.");

                return this.Page();
            }

            return RedirectToPage("Index");
        }
    }
}