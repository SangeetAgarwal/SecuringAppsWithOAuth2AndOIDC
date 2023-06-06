using System.Text.Json;
using MakeBitByte.IDP.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeBitByte.IDP.DbContexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }         

        public UserDbContext(
          DbContextOptions<UserDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Subject)
            .IsUnique();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"),
                    Password = "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==",
                    Subject = "36c434ff-888f-4820-a316-1a025f8a4f0c",
                    UserName = "Vinita",
                    Email = "vinita@anemail.com",
                    Active = true
                },
                new User()
                {
                    Id = new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"),
                    Password = "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==",
                    Subject = "da0d5f9a-8217-4a68-8398-db8506fdf618",
                    UserName = "Appa",
                    Email = "app@anemail.com",
                    Active = true
                },
                new User()
                {
                    Id = new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"),
                    Password = "AQAAAAEAACcQAAAAEAz2SA93l305Govh36XNZX7QLHAYUfL1bGTgO+2sSwOgKwSkcQiUD9ir4fG8U4E1GA==",
                    Subject = "1c94f897-2af3-46d4-a131-8a147d18a2f2",
                    UserName = "Arjun",
                    Email = "arjun@anemail.com",
                    Active = true
                });

            modelBuilder.Entity<UserClaim>().HasData(
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"),
                 Type = "given_name",
                 Value = "Vinita"
             },
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"),
                 Type = "family_name",
                 Value = "Agarwal"
             }, 
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"),
                 Type = "subscriberSince",
                 Value = JsonSerializer.Serialize(new DateTime(2021, 06, 21))
             },
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("a271cb6d-19de-4786-a508-e1a15f0d3f97"),
                 Type = "role",
                 Value = "pro"
             },

             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"),
                 Type = "given_name",
                 Value = "Appa"
             },
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"),
                 Type = "family_name",
                 Value = "Agarwal"
             }, 
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"),
                 Type = "subscriberSince",
                 Value = JsonSerializer.Serialize(new DateTime(2023, 01, 21)) 
             }, 
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("4b358d3f-d55e-4771-8e5b-5aeb62cb3e57"),
                 Type = "role",
                 Value = "pro"
             },

             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"),
                 Type = "given_name",
                 Value = "Arjun"
             },
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"),
                 Type = "family_name",
                 Value = "Agarwal"
             }, 
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"),
                 Type = "subscriberSince",
                 Value = JsonSerializer.Serialize(DateTime.MaxValue) 
             }, 
             new UserClaim()
             {
                 Id = Guid.NewGuid(),
                 UserId = new Guid("65ceedf4-43c8-4818-80d8-9ce2bb96c231"),
                 Type = "role",
                 Value = "none"
             });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // get updated entries
            var updatedConcurrencyAwareEntries = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified)
                    .OfType<IConcurrencyAware>();

            foreach (var entry in updatedConcurrencyAwareEntries)
            {
                entry.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
