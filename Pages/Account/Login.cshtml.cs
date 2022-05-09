/*
 * added razor page under custom Account folder under Pages For login
 * Non-scaffolded empty razor page -- based on mvc framework
 * but gives a MVVM (model-view-viewmodel) feel
 * also automatically generates related Login.cshtml page
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Kaoru_Art_Gallery.Pages.Account
{
    public class LoginModel : PageModel
    {

        // create model to communicate between front and back end
        // on login, credentials gets sent here
        [BindProperty]
        public Credential? Credential { get; set; }
        public void OnGet()
        {
        }

        // received event here through OnPost method by convention
        // credentials (username, password) are verified here
        public async Task<IActionResult> OnPostAsync()
        {
            // verify if credentials are valid
            if (!ModelState.IsValid) return Page();
            // verify credential
            // these credentials are sample only and would never
            // be hard-coded in like this
            // needs to be dynamic and retrieved from a db in a real world app
            if (Credential.UserName == "linden" && Credential.Password == "password")
            {
                // Creating the security context
                // claims are just a pair of attributes
                var claims = new List<Claim> {
                    // claim params: claim type: String, claim value: string
                    new Claim(ClaimTypes.Name, "linden"),
                    new Claim(ClaimTypes.Email, "linden@mywebsite.com"),
                    // claim for HRManager in Settings.cshtml.cs and Program.cs.
                    // builder.Services.AddAuthorization **MAKE SURE TO CLEAR COOKIES AND RE-LOGIN
                    // TO SEE EFFECT
                    // can now access Settings view
                    // first param of Claim must be same string as policy => policy.RequireClaim("")
                    // in Program.cs.AddAuthorization line 50
                     new Claim("Manager", "true"),
                    // claim for HR
                    // claim Parameters need to be the same as Program.cs. 
                    // builder.Services.AddAuthorization Policy line 40
                    // now I can access HumanResources page **MAKE SURE TO CLEAR COOKIES AND RELOGIN
                    new Claim("Department", "HR"),
                    // claim for admin in Settings.cshtml.cs and Program.cs.
                    // builder.Services.AddAuthorization **MAKE SURE TO CLEAR COOKIES AND RELOGIN
                    // can now access Settings view
                     new Claim("Admin", "true"),

                     // claim for emplyment date in the case that an employee
                     // needs to be passed a mock 3-month probationary period to have access
                     // see dev-created Authorization folder in working directory
                     // where custom requirements are stored
                     new Claim("EmploymentDate", "2021-05-01")
                    };

                // add claims to an identity
                // identity with specified auth type
                // Parameters:
                //   claims Claims:
                //     The claims with which to populate the claims identity.
                //
                //   authenticationType String:
                //     The type of authentication used.
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                //claims principle which is security context
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                // before creating cookie, configure Authentication properties
                // which includes cookie settings like IsPersistent
                // passed as third param in SignInAsync
                var authProperties = new AuthenticationProperties
                {
                    // make the cookie persistent only if the user selects this option in the check box
                    // in Login.cshtml
                   IsPersistent = Credential.RememberMe
                };
                // serialize the claims principle into a Cookie string,
                // encrypt the string and save it as a cookie
                // in HttpContext --need sign-in authentication handler
                // need to inject AddAuthentication() handlers from IAuthenticationService Interface
                // IAuthenticationService Interface contains all abstraction authentication needs
                /**
                 * Cookies come from here. When correct credentials are entered,
                 * SignInAsync is triggered and it talks to IAuthenticationService Interface
                 */
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                // if everything works out, return index view
                return RedirectToPage("/index");
            }

            // if it fails wrong password, etc. , return the same login page
            return Page();
        }
    }

    // this class is used as a model to communicate between
    // razor pages and page model
    public class Credential
    {
        //validation
        [Required]
        [Display(Name ="User Name")]
        public string? UserName { get; set; }
        [Required]
        // data type password
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }    
    }
}
