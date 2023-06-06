using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Notes.MvcApp.Controllers;

public class AuthenticationController : Controller
{
    [Authorize]
    public async Task Logout()
    {
        // Clears the local cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirect to IDP so it can clear its own cookie
        // Remember: the client app doesn't own this cookie, the IDP does so 
        // in theory only the IDP should remove this cookie.
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}