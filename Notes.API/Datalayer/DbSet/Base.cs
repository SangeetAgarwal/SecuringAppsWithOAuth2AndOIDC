using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.API.Datalayer.DbSet
{
    public abstract class Base
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid UserId { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
