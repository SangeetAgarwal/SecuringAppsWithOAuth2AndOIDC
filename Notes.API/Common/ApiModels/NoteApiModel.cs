using System.ComponentModel.DataAnnotations;

namespace Notes.API.Common.ApiModels
{
    public class NoteApiModel
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
