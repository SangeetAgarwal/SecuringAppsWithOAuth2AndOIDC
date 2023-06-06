using System.Globalization;
using System.Net;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using IdentityModel;
using MakeBitByte.IDP.Entities;
using MakeBitByte.IDP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MakeBitByte.IDP.Pages.User.Registration
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class IndexModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILocalUserService _localUserService;

        public IndexModel(IIdentityServerInteractionService interaction, ILocalUserService localUserService)
        {
            _interaction = interaction;
            _localUserService = localUserService;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        //  called by anchor tag in .cshtml calling via asp-route-returnUrl 
        //  and, asp-page is where we are now i.e. User/Registration/Index
        public IActionResult OnGet(string returnUrl)
        {
            BuildModel(returnUrl);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                BuildModel(Input.ReturnUrl);
                return Page();
            }

            var user = new Entities.User
            {
                UserName = Input.UserName,
                Subject = Guid.NewGuid().ToString(),    
                Active = false,
                Email = Input.Email,
            };

            user.Claims.Add(new UserClaim
            {
                Type = JwtClaimTypes.GivenName,
                Value = Input.GivenName
            });

            user.Claims.Add(new UserClaim
            {
                Type = JwtClaimTypes.FamilyName,
                Value = Input.FamilyName
            });

            user.Claims.Add(new UserClaim
            {
                Type = "role",
                Value = "none"
            });

            user.Claims.Add(new UserClaim()
            {
                Type = "subscriberSince",
                Value = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
            });

            _localUserService.AddUser(user, Input.Password);
            await _localUserService.SaveChangesAsync();


            var activationLink = Url.PageLink("/user/activation/index", values: new { activationCode = user.SecurityCode });

            Console.WriteLine(activationLink);

            return Redirect("~/User/ActivationCodeSent/Index");

            //var isUser = new IdentityServerUser(user.Subject)
            //{
            //    DisplayName = user.UserName,
                
            //};
            //await HttpContext.SignInAsync(isUser);

            //if (_interaction.IsValidReturnUrl(Input.ReturnUrl) ||
            //    Url.IsLocalUrl(Input.ReturnUrl))
            //{
            //    return Redirect(Input.ReturnUrl);
            //}
            
            return Redirect("~/");
        }

        private void BuildModel(string returnUrl)
        {
            Input = new InputModel
            {
                UserName = string.Empty,
                Password = string.Empty,
                ReturnUrl = returnUrl
            };
        }
    }
}
