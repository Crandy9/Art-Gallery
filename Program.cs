// Razor Pages no longer using Startup.cs file that comes with asp.net core MVC template solution
// builder configures the HTTP pipeline, routes, middleware, etc.
// CreateBuilder(args) returns web app with preconfigured defaults
using Kaoru_Art_Gallery.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

/*** Add services to the container here ***/
/*LEFT OFF HERE https://www.youtube.com/watch?v=sogS0DtejVA&list=WL&index=1 
 * @ 1:41:50**/

// Inject Authentication handler
// adding cookie handler, handles enryption/decryption, etc.
// "MyCookieAuth" Authentication scheme name
// AddAuthentication("MyCookieAuth") provides authentication scheme name string to tell Authentication middleware
// which authentication scheme we want to use to do authentication
/**
 * NOTE: There could be multiple different authnetication handlers, this is just one example (cookies, tokens, etc.)
 * */
// "MyCookieAuth" is the Application cookie name found in the dev tools f12 in the browser
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    // specifies cookie name which is the most important aspect in authentication
    // lets asp.net core know which cookies contains authentication security context
    // IMPORTANT:
    // this cookie name needs to be the same for AddCookie("MyCookieAuth") scheme name above,
    // var identity in Pages/Account/Login.cshtml.cs Line 38,
    // SignInAsync param scheme name in Login.cshtml.cs Line 51
    options.Cookie.Name = "MyCookieAuth";
    //*** you can specify login redirect view here if it is not already created and named in /Account/Login
    // which is the default naming convention recognized by asp.net
    // example: KaoruWebAuthentication could be the folder name under Pages,
    // Login refers to Login.cshtml.cs razor page and subsequent View that you have to manually add 
    // ex.) options.LoginPath = "/KaoruWebAuthentication/Login"
    // used when anonymous authentication is unacceptable to open View
    //*** you can specify access denied page here as well, when a user tries to
    // access a View that they do not have authorization for
    // if you don't have it set up, the browser will give an HTTP ERROR 404 page not found
    // have to give path and create the razor page with view
    options.AccessDeniedPath = "/Account/AccessDenied";

    // specify cookie lifetime, doesn't remove cookie like logout does,
    // but simulates cookie deletion on expiration time, yet cookie is still stored
    // cookie lifetime is constrained by the browser session lifetime, but can override 
    // by using a persistent cookie by using a check box "remember me"
    // each time you do a new browser session, the TimeSpan restarts
    // options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
});

// Inject Authorization handler -- double lambda implementation 
// add policies which are used for authorization access to certain pages
// can make admin only page, etc.
builder.Services.AddAuthorization(options =>
{
    // AdminOnly policy for Settings.cshtml.cs
    options.AddPolicy("AdminOnly",
        policy => policy.RequireClaim("Admin"));

    // HRManager policy for HRManager.cshtml.cs
    // chaining this policy with MustBelongToHRDepartment policy
    // to ensure that this user not only belongs to HR,but is also the HR manager
    // both need to be true in var claims in Login.cshtml.cs
    // in order to get access to these Views
    // AddPolicy("HRManagerOnly" param must be same name as
    // [Authorize(Policy = "MustBelongToHRDepartment")] in HRManager.cshtml.cs
  /*  options.AddPolicy("HRManagerOnly",
        policy => policy
        .RequireClaim("Department", "HR")
        .RequireClaim("Manager")
        .Requirements.Add(new HRManagerProbationRequirement(3)));

    // apply this policy to HumanResource.cshtml.cs
    options.AddPolicy("MustBelongToHRDepartment",
        // adding this claim: RequireClaim("Department", "HR")) to 
        // this policy: "MustBelongToHRDepartment"
        policy => policy.RequireClaim("Department", "HR"));

    */
});
/*
builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();*/

builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*  ******* MIDDLEWARE ******** */
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
/**
 * 
 * UNDER THE HOOD UseAuthentication() 
 */
// inserts authentication middleware to the pipeline
// responsible to call authentication handler
// from IAuthenticationService interface
// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.iauthenticationservice?view=aspnetcore-6.0
// calls AuthenticateAsync(HttpContext, String) method
// which returns the AuthenticateResult i.e. translates the cookie 
// to the security context of the claims prinicple
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// launches web app
app.Run();
