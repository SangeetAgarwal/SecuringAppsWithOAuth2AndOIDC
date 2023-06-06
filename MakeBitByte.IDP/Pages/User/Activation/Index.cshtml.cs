using MakeBitByte.IDP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MakeBitByte.IDP.Pages.User.Activation
{

    [AllowAnonymous]
    [SecurityHeaders]
    public class IndexModel : PageModel
    {
        private readonly ILocalUserService _localUserService;

        public IndexModel(ILocalUserService localUserService)
        {
            _localUserService = localUserService ?? throw new ArgumentNullException(nameof(localUserService));
        }

        public async Task<IActionResult> OnGet(string activationCode)
        {
            Input = new InputModel();

            if (await _localUserService.CheckActivationCodeAsync(activationCode))
            {
                Input.Message = "You have been successfully activated";
            }
            else
            {
                Input.Message = "Please contact your administrator";
            }

            await _localUserService.SaveChangesAsync();

            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }
    }
}
