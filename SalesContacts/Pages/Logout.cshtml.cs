using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesContacts.Data.Model;

namespace SalesContacts.Pages
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private SignInManager<UserSys> signInManager;

        public LogoutModel(SignInManager<UserSys> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await this.signInManager.SignOutAsync();

            return RedirectToPage("Login");
        }
    }
}