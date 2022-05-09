// dev-created class
// where custom requirements are stored

using Microsoft.AspNetCore.Authorization;

namespace Kaoru_Art_Gallery.Authorization
{
    // using IAuthorizationRequirement interface to implement handler properly
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public HRManagerProbationRequirement(int probationMonths)
        {
            probationMonths = probationMonths;
        }

        public int ProbationMonths { get; }
    }

    // handler abstract class
    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        // virtual method
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            HRManagerProbationRequirement requirement)
        {
            // get the employment date in the identity claim of Login.cshtml.cs
            // x => x.Type == "EmploymentDate" is read:
            // is there is not a claim x whose type is EmploymentDate
            if (!context.User.HasClaim(x => x.Type == "EmploymentDate"))
                // if requirements don't exist
                return Task.CompletedTask;

            // if there is an employment claim, get date
            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);

            // compare differences 
            var period = DateTime.Now - empDate;

            if (period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
