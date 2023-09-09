using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Notes.MvcApp.Interfaces;
using Notes.MvcApp.Models;
using Notes.MvcApp.Services;

namespace Notes.MvcApp.Controllers
{
    [Authorize(AuthenticationSchemes = "CodeFlowWithTokenEncryptionScheme")]
    // [Authorize(AuthenticationSchemes = "CodeFlowWithPrivateKeyJWTAndJARScheme")]
    // [Authorize(AuthenticationSchemes = "CodeFlowWithJARScheme")]
    // [Authorize(AuthenticationSchemes = "CodeFlowWithPrivateKeyJWTScheme")]
    // [Authorize]
    public class NoteController : Controller
    {
        private readonly INotesService _notesService;
        private readonly ILogger<NoteController> _logger;
        public NoteController(INotesService notesService, ILogger<NoteController> logger)
        {
            _notesService = notesService;
            _logger = logger;
        }
        public async Task<ActionResult> Index()
        {
            await LogIdentityInfo();
            return View(await _notesService.GetNotesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var note =  await _notesService.GetNoteAsync(id);

            return View(note);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NoteViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(model);

            await _notesService.UpdateNoteAsync(model);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _notesService.GetNoteAsync(id));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _notesService.DeleteNoteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new NoteViewModel {Id = Guid.NewGuid()});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _notesService.AddNoteAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        [Authorize(Policy = "CanSearch")]
        public IActionResult Search()
        {
            return View(new SearchModel()); 
        }

        [HttpPost]
        [Authorize(Policy = "CanSearch")]
        public async Task<IActionResult> Search(string searchText)
        {
            var notes = await _notesService.SearchAsync(searchText);

            var searchModel = new SearchModel
            {
                SearchResult = notes,
                SearchText = searchText
            };

            return View(searchModel);
        }

        public async Task LogIdentityInfo()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userClaimsStringBuilder = new StringBuilder();

            foreach (var claim in User.Claims)
            {
                userClaimsStringBuilder.AppendLine($"claim type: {claim.Type} - claim value: {claim.Value}");
            }

            _logger.LogInformation($"Identity toke and user claims \n " +
                                   $"{identityToken} \n " +
                                   $"{userClaimsStringBuilder}");
            _logger.LogInformation($"AccessToken \n" +
                                   $"{accessToken} \n ");
        }
    }

}
