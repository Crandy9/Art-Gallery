/*
 * added razor page under Pages For Authorized user
 * Non-scaffolded empty razor page -- based on mvc framework
 * but gives a MVVM (model-view-viewmodel) feel
 * also automatically generates related HumanResource.cshtml page
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaoru_Art_Gallery.Pages
{
    // ctrl + . to import [Authorize] attribute namespace
    // Policy string name must be defined in Program.cs. 
    // builder.Services.AddAuthorization Line 34
    // ensures that user must have claim that satisfies this policy
    // policy is asking for requireclaim value to be HR
    // see Login.cshtml.cs var claims
    [Authorize(Policy = "MustBelongToHRDepartment")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
