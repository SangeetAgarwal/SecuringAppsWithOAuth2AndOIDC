
using System.ComponentModel.DataAnnotations;

namespace Notes.API.Datalayer.DbSet
{
    public class Note : Base
    {
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
