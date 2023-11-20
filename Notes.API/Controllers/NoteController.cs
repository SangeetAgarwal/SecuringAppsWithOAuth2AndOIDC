using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.API.Common.ApiModels;
using Notes.API.Common.Interfaces.Servicelayer;
using Notes.API.Datalayer.DbSet;
using Notes.Api.DPoP;

namespace Notes.API.Controllers
{

    public class NoteController : BaseController
    {
        public NoteController(IDatalayerService datalayerService, ILogger<BaseController> logger) : base(datalayerService, logger)
        { }

        [HttpGet("GetNotes")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NoteApiModel>>> GetNotes()
        {
            //var proofToken = Request.GetDPoPProofToken();
            //if (proofToken == null) return BadRequest();
            
            var notes = await DatalayerService.GetNotesAsync();
            return Ok(notes);
        }

        [HttpGet("GetNote/{id:guid}")]
        [Produces("application/json")]
        [Authorize("MustOwnResource")]
        public async Task<ActionResult<NoteApiModel>> GetNote([FromRoute] Guid id)
        {
            // var proofToken = Request.GetDPoPProofToken();
            // if (proofToken == null) return BadRequest();

            var note = await DatalayerService.GetNoteAsync(id);

            return Ok(note);
        }

        [Authorize("MustOwnResource")]
        [HttpDelete("DeleteNote/{id:guid}")]
        public async Task<ActionResult> DeleteNote([FromRoute] Guid id)
        {

            // var proofToken = Request.GetDPoPProofToken();
            // if (proofToken == null) return BadRequest();

            await DatalayerService.DeleteNoteAsync(id);

            return Ok();
        }

        [HttpPost("AddNote")]
        [Authorize(Policy = "ClientAppCanWrite")]
        public async Task<ActionResult> AddNote([FromBody] NoteApiModel note)
        {

            // var proofToken = Request.GetDPoPProofToken();
            // if (proofToken == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await DatalayerService.AddNoteAsync(note);

            return Ok();
        }

        [HttpPut("UpdateNote/{id:guid}")]
        [Authorize(Policy = "ClientAppCanWrite")]
        [Authorize("MustOwnResource")]
        public async Task<ActionResult> UpdateNote([FromRoute] Guid id, [FromBody] NoteApiModel note)
        {
            // var proofToken = Request.GetDPoPProofToken();
            // if (proofToken == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await DatalayerService.UpdateNoteAsync(note);

            return Ok();
        }


        [Authorize(Policy = "CanSearch")]
        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<NoteApiModel>>> Search([FromBody] string searchText)
        {

            // var proofToken = Request.GetDPoPProofToken();
            // if (proofToken == null) return BadRequest();

            var notes = await DatalayerService.Search(searchText);

            return Ok(notes);
        }
    }
}
