using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaoru_Art_Gallery.Pages
{
    // [authorize] attribute denies anonymous identity (non-signed in users)
    // since Index.cshtml view is the "homepage" and since the authorize attribute
    // is enabled, if you try to access this page as an anonymous user (no cookie), it will
    // redirect to Login.cshtml view
    // on startup, server looks for an authentication cookie, if it fails to find one, 
    // user is redirected to configured logon page specified by LoginUrl
    // the reason authorization middleware knows to redirect to Login.cshtml
    // is because it looks for Account folder/Login.cshtml by default which is asp.net core convention
    // if we didn't have the Account folder and Login.cshtml view, the browser would
    // return an HTTP Error 404
    // to explicitly set the login page location, set it up in Program.cs 
    // builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options
    // [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // get handler of Index page
        public void OnGet()
        {

        }
    }
}