namespace Notes.MvcApp.Models
{
    public class SearchModel
    {
        public string? SearchText { get; set; }

        public IEnumerable<NoteViewModel>? SearchResult { get; set; }
    }
}
