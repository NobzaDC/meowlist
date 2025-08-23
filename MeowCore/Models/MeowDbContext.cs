using Azure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MeowCore.Models
{
    public class MeowDbContext: DbContext
    {
        public MeowDbContext(DbContextOptions<MeowDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Lists> Lists { get; set; }
        public DbSet<Todos> Todos { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<TodosTags> TodoTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodosTags>()
                .HasKey(tt => new { tt.TodoId, tt.TagId });

            modelBuilder.Entity<TodosTags>()
                .HasOne(tt => tt.Todo)
                .WithMany(t => t.TodosTags)
                .HasForeignKey(tt => tt.TodoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TodosTags>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TodoTags)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Todos>()
                .Property(t => t.Status)
                .HasConversion<string>();

            //Password Hash for Admin123!
            var adminPasswordHash = "$2a$11$ZI3qmhCXbEL7QNhx.TdTOOHiCHztLTQGx8zHev67CTwR9GD3VWzKm"; 

            //Password Hash for abc123!
            var generalDefaultPassword = "$2a$11$9MDPMI3a.VQCvB3AXqgrkuibpkJ4QRAmGdSzXiIAP5jgEE/QsU2Qm";

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, Email = "catmin@meowlist.pur", Name = "admin", DisplayName = "Catmin", IsAdmin = true, PasswordHash = adminPasswordHash },
                new Users { Id = 2, Email = "fishlover@meowlist.pur", Name = "Luna", DisplayName = "FishLover", IsAdmin = false, PasswordHash = generalDefaultPassword },
                new Users { Id = 3, Email = "purrington@meowlist.pur", Name = "Milo", DisplayName = "Purrington", IsAdmin = false, PasswordHash = generalDefaultPassword },
                new Users { Id = 4, Email = "softpaw@meowlist.pur", Name = "Nala", DisplayName = "Softpaw", IsAdmin = false, PasswordHash = generalDefaultPassword }
            );
        }
    }
}
