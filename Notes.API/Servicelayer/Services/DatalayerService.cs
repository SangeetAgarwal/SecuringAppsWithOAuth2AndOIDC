using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Api.ClaimsPrincipal;
using Notes.API.Common.Interfaces.Servicelayer;
using Notes.API.Datalayer.Context;
using NoteApiModel = Notes.API.Common.ApiModels.NoteApiModel;

namespace Notes.API.Servicelayer.Services
{
    public class DatalayerService : IDatalayerService
    {
        private readonly DatalayerContext _datalayerContext;
        private readonly IMapper _mapper;

#pragma warning disable IDE0052
        private readonly ILogger<DatalayerService> _logger;
        private readonly ClaimsProvider _claimsProvider;
#pragma warning restore IDE0052

        public DatalayerService(DatalayerContext datalayerContext, IMapper mapper, ILogger<DatalayerService> logger, ClaimsProvider claimsProvider)
        {
            _datalayerContext = datalayerContext;
            _mapper = mapper;
            _logger = logger;
            _claimsProvider = claimsProvider;
        }

        public async Task<IEnumerable<NoteApiModel>> GetNotesAsync()
        {
            //return new List<NoteApiModel>();
            var subjectClaim = (_claimsProvider.ClaimsPrincipal?.Claims ?? Array.Empty<Claim>()).SingleOrDefault(r => r.Type == "sub");

            if (subjectClaim == null)
            {
                throw new ArgumentNullException(nameof(subjectClaim));
            }

            var subject = subjectClaim.Value;

            var notes = await _datalayerContext.Notes.Where(r => r.UserId == Guid.Parse(subject)).ToListAsync();
            var mappedNotes = _mapper.Map<List<NoteApiModel>>(notes);
            return mappedNotes;
        }

        public async Task<bool> IsNoteOwnerAsync(Guid id, Guid userId)
        {
            return await _datalayerContext.Notes.AnyAsync(r => r.UserId == userId && r.Id == id);
        }

        public async Task<NoteApiModel> GetNoteAsync(Guid id)
        {
            var note = await _datalayerContext.Notes.FirstOrDefaultAsync(r => r.Id == id);
            if (note == null)
            {
                throw new ApplicationException($"note with note id {id} doesn't exist");
            }

            return _mapper.Map<NoteApiModel>(note);
        }

        public async Task<bool> NoteExistsAsync(Guid id)
        {
            return await _datalayerContext.Notes.AnyAsync(r => r.Id == id);
        }

        public async Task AddNoteAsync(NoteApiModel note)
        {

            var subjectClaim = (_claimsProvider.ClaimsPrincipal?.Claims ?? Array.Empty<Claim>()).SingleOrDefault(r => r.Type == "sub");

            if (subjectClaim == null)
            {
                throw new ArgumentNullException(nameof(subjectClaim));
            }

            var subject = subjectClaim.Value;

            
            var mappedNote = _mapper.Map<Datalayer.DbSet.Note>(note);

            mappedNote.UserId = Guid.Parse(subject);

            _datalayerContext.Notes.Add(mappedNote);
            await _datalayerContext.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(NoteApiModel note)
        {
            var dbNote = await _datalayerContext.Notes.SingleOrDefaultAsync(r => r.Id == note.Id);
            if (dbNote == null)
            {
                throw new ApplicationException($"note with id {note.Id} not found");
            }
            _mapper.Map<NoteApiModel, Datalayer.DbSet.Note>(note, dbNote);
            await _datalayerContext.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var dbNote = await _datalayerContext.Notes.SingleOrDefaultAsync(r => r.Id == id);
            if (dbNote == null)
            {
                throw new ApplicationException($"note with id {id} not found");
            }

            _datalayerContext.Remove(dbNote);
            await _datalayerContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoteApiModel>> Search(string searchText)
        {
            var notes = await _datalayerContext.Notes.Where(r=> r.Title.Contains(searchText)).ToListAsync();
            var mappedNotes = _mapper.Map<List<NoteApiModel>>(notes);
            return mappedNotes;
        }
    }
}
