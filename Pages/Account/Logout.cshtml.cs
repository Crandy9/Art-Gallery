using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

/**
 * dev-created file Login.cshtml for creating logout manually
To navigate to this page, the razor pages are organized by folder so go to/account/logout
No UI needed so logout.cshtml can be left alone
 */

namespace Kaoru_Art_Gallery.Pages.Account
{
    public class LogoutModel : PageModel
    {
        // don't need this method
        public void OnGet()
        {
        }

        // for logout, use an onpost method
        // implmenent Task<IActionResult> because we need to redirect
        public async Task<IActionResult> OnPostAsync()
        {
            // sign out functionality, requires using Microsoft.AspNetCore.Authentication;
            // scheme name must be same as scheme name in program.cs addauth method
            // NOTE: use a constant instead of manually entering scheme name each time to avoid typos
            // removes cookie MyCookieAuth from browser to "log user out"
            await HttpContext.SignOutAsync("MyCookieAuth");
            // on logout, just return to homepage or any page you want
            return RedirectToPage("/Index");

        }
    }
}
