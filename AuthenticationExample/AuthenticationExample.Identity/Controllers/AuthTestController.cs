using AuthenticationExample.Identity.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationExample.Identity.Controllers
{
    public class AuthTestController : ControllerBase
    {
        [Authorize]
        public IActionResult TestAuthorize() => Ok();

        [Authorize(Roles = SystemRoles.Admin)]
        public IActionResult TestAdminAuthorize() => Ok();

        [Authorize(Policy = SystemPolicies.MinimumYearWorked)]
        public IActionResult TestMinimumYearWorkedPolicy() => Ok();
    }
}
