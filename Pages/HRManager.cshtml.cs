using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaoru_Art_Gallery.Pages
{
    // HRManager Policy configured in Program.cs AddAuthorization method
    // and in Login.cshtml.cs public async Task<IActionResult> OnPostAsync()
    // var claims
    // (Policy = "HRManagerOnly") needs to match first param of
    // options.AddPolicy("HRManagerOnly",... in Program.cs.AddAuthorization line 44
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
