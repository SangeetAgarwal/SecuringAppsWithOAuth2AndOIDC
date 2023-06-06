using System.Net.Mime;
using Notes.API.Common.ApiModels;

namespace Notes.API.Common.Interfaces.Servicelayer
{
    public interface IDatalayerService
    {
        Task<IEnumerable<NoteApiModel>> GetNotesAsync();
        Task<bool> IsNoteOwnerAsync(Guid id, Guid userId);
        Task<NoteApiModel> GetNoteAsync(Guid id);
        Task<bool> NoteExistsAsync(Guid id);
        Task AddNoteAsync(NoteApiModel note);
        Task UpdateNoteAsync(NoteApiModel note);
        Task DeleteNoteAsync(Guid id);
        Task<IEnumerable<NoteApiModel>> Search(string searchText);

    }
}
