using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaoru_Art_Gallery.Pages
{
    // AdminOnly Policy configured in Program.cs
    // and in Login.cshtml.cs public async Task<IActionResult> OnPostAsync()
    // var claims
    [Authorize (Policy = "AdminOnly")]
    public class SettingsModel : PageModel
    {   
        public void OnGet()
        {
        }
    }
}
