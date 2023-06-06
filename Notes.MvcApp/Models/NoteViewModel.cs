using System.ComponentModel.DataAnnotations;

namespace Notes.MvcApp.Models
{
    public class NoteViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(200)] [Required] 
        public string Title { get; set; } = null!;

        [MaxLength(500)] [Required] 
        public string Description { get; set; } = null!;

        [Required] 
        public string Content { get; set; } = null!;
    }
}
