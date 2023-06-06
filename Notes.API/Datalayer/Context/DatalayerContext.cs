using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Notes.API.Datalayer.DbSet;
using Notes.API.Datalayer.Extensions;

namespace Notes.API.Datalayer.Context
{
    public class DatalayerContext : DbContext
    {

        //private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Note> Notes { get; set; } = null!;

        public DatalayerContext(DbContextOptions<DatalayerContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Base).IsAssignableFrom(type.ClrType))
                    modelBuilder.HasSoftDeleteQueryFilter(type.ClrType);
            }

            modelBuilder.Entity<Note>().HasData(
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Arjun) consectetur adipiscing elit",
                    Description = "Lorem ipsum",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                              " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Amet porttitor eget dolor morbi non. Varius vel pharetra vel turpis nunc eget." +
                              " Augue ut lectus arcu bibendum at varius. Accumsan tortor posuere ac ut. " +
                              "In tellus integer feugiat scelerisque varius morbi enim nunc. Turpis massa" +
                              " sed elementum tempus egestas sed sed risus. Volutpat consequat mauris nunc" +
                              " congue. Mauris commodo quis imperdiet massa tincidunt nunc. Mattis rhoncus" +
                              " urna neque viverra. Arcu odio ut sem nulla pharetra diam sit amet. Sagittis " +
                              "nisl rhoncus mattis rhoncus urna neque viverra justo nec. Etiam tempor orci eu " +
                              "lobortis elementum nibh tellus. Cursus vitae congue mauris rhoncus aenean.",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2"),

                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Arjun) Erat pellentesque adipiscing commodo elit at imperdiet dui accumsan.",
                    Description = "Nunc lobortis mattis aliquam faucibus purus in ",
                    Content = "Nunc lobortis mattis aliquam faucibus purus in " +
                              "massa tempor. Ultricies leo integer malesuada nunc. " +
                              "Eget duis at tellus at urna. Aenean vel elit scelerisque " +
                              "mauris pellentesque pulvinar. Quisque id diam " +
                              "vel quam elementum pulvinar etiam non quam. " +
                              "Lectus urna duis convallis convallis tellus id interdum velit.  ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2"),

                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Arjun) consectetur adipiscing elit",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit,",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                              " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                              "Amet porttitor eget dolor morbi non. Varius vel pharetra vel turpis nunc eget." +
                              " Augue ut lectus arcu bibendum at varius. Accumsan tortor posuere ac ut. " +
                              "In tellus integer feugiat scelerisque varius morbi enim nunc. Turpis massa" +
                              " sed elementum tempus egestas sed sed risus. Volutpat consequat mauris nunc" +
                              " congue. Mauris commodo quis imperdiet massa tincidunt nunc. Mattis rhoncus" +
                              " urna neque viverra. Arcu odio ut sem nulla pharetra diam sit amet. Sagittis " +
                              "nisl rhoncus mattis rhoncus urna neque viverra justo nec. Etiam tempor orci eu " +
                              "lobortis elementum nibh tellus. Cursus vitae congue mauris rhoncus aenean.",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2"),

                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Arjun) Amet risus nullam eget felis eget nunc lobortis mattis ",
                    Description = "Amet risus nullam eget felis eget nunc lobortis mattis ",
                    Content = "Amet risus nullam eget felis eget nunc lobortis mattis " +
                              "aliquam. Consectetur adipiscing elit duis tristique sollicitudin" +
                              " nibh sit amet. Faucibus a pellentesque sit amet porttitor eget" +
                              " dolor morbi. Pellentesque habitant morbi tristique senectus et" +
                              " netus et. Quam viverra orci sagittis eu volutpat odio facilisis." +
                              " Facilisi cras fermentum odio eu.  ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2"),

                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Arjun) In est ante in nibh mauris cursus",
                    Description = "In est ante in nibh mauris cursus. Massa tempor nec feugiat nisl pretium fusce id. ",
                    Content = "In est ante in nibh mauris cursus. Massa tempor nec feugiat nisl pretium fusce id. " +
                              "Morbi quis commodo odio aenean sed adipiscing. Pretium viverra suspendisse potenti" +
                              " nullam ac tortor vitae purus. Placerat in egestas erat imperdiet.",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("1c94f897-2af3-46d4-a131-8a147d18a2f2"),

                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Senectus et netus et malesuada fames ac turpis",
                    Description = "Senectus et netus et malesuada fames ac turpis. ",
                    Content = "Senectus et netus et malesuada fames ac turpis. " +
                              "Faucibus turpis in eu mi bibendum neque. Rhoncus aenean vel elit " +
                              "scelerisque mauris pellentesque pulvinar. Vitae congue mauris rhoncus " +
                              "aenean vel elit scelerisque. ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Faucibus a pellentesque sit amet. Adipiscing elit ",
                    Description = "Faucibus a pellentesque sit amet. Adipiscing elit ",
                    Content = "Faucibus a pellentesque sit amet. Adipiscing elit " +
                              "pellentesque habitant morbi tristique. Cras semper auctor neque " +
                              "vitae tempus. Facilisis magna etiam tempor orci eu lobortis elementum nibh. " +
                              "Mattis vulputate enim nulla aliquet porttitor lacus luctus accumsan. ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat",
                    Description = "Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat. ",
                    Content = "Aenean sed adipiscing diam donec adipiscing tristique risus nec feugiat. " +
                              "In egestas erat imperdiet sed euismod nisi porta. Eros donec ac odio tempor orci " +
                              "dapibus ultrices. Amet nisl suscipit adipiscing bibendum est. Neque convallis a " +
                              "cras semper auctor neque. Ut venenatis tellus in metus vulputate eu scelerisque. ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Eu mi bibendum neque egestas congue quisque egestas.",
                    Description = "Eu mi bibendum neque egestas congue quisque egestas. ",
                    Content = "Eu mi bibendum neque egestas congue quisque egestas. " +
                              "At quis risus sed vulputate odio. Nunc pulvinar sapien et ligula ullamcorper malesuada " +
                              "proin libero. Elit at imperdiet dui accumsan sit amet nulla facilisi morbi. Mauris " +
                              "pellentesque pulvinar pellentesque habitant morbi tristique senectus et. " +
                              "A diam maecenas sed enim.` ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Id interdum velit laoreet id donec ultrices tincidunt arcu. ",
                    Description = "Id interdum velit laoreet id donec ultrices tincidunt arcu. ",
                    Content = "Id interdum velit laoreet id donec ultrices tincidunt arcu. " +
                              "Consequat nisl vel pretium lectus quam id. Tellus mauris a diam maecenas sed enim ut sem. " +
                              "Ullamcorper malesuada proin libero nunc consequat interdum. Nibh sit amet commodo nulla " +
                              "facilisi nullam. Tristique nulla aliquet enim tortor at auctor.",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Ultrices sagittis orci a scelerisque purus semper.",
                    Description = "Ultrices sagittis orci a scelerisque purus semper. ",
                    Content = "Ultrices sagittis orci a scelerisque purus semper. " +
                              "Maecenas sed enim ut sem viverra aliquet eget sit amet. " +
                              "Dui sapien eget mi proin sed libero. Donec et odio pellentesque diam " +
                              "volutpat commodo. Ultrices gravida dictum fusce ut. Diam vulputate " +
                              "ut pharetra sit amet aliquam id diam maecenas. Tristique sollicitudin nibh sit amet. ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    Title = "(A note by Vinita) Senectus et netus et malesuada fames ac turpis",
                    Description = " Senectus et netus et malesuada fames ac turpis. ",
                    Content = " Senectus et netus et malesuada fames ac turpis. " +
                              "Faucibus turpis in eu mi bibendum neque. Rhoncus aenean vel elit " +
                              "scelerisque mauris pellentesque pulvinar. Vitae congue mauris rhoncus " +
                              "aenean vel elit scelerisque. ",
                    DateCreated = new DateTime(2023, 1, 15),
                    LastModified = null,
                    UserId = new Guid("36c434ff-888f-4820-a316-1a025f8a4f0c"),
                }

            );

            base.OnModelCreating(modelBuilder);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            OnBeforeSaving();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {

            //var httpContext = _httpContextAccessor.HttpContext;

            //var userId = httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (userId == null)
            //{
            //    throw new Exception("User Id is null");
            //}

            //var userIdGuid = Guid.Parse(userId);

            foreach (var entry in ChangeTracker.Entries<Base>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DeleteFlag = false;
                        entry.Entity.DateCreated  = DateTime.Now;
                        //entry.Entity.UserId = userIdGuid;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeleteFlag = true;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
