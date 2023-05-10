using KanbanWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.DB
{
    public class KanbanDBContext : DbContext
    {
        public KanbanDBContext(DbContextOptions<KanbanDBContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<KanbanTask> KanbanTasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasMany(m => m.TasksCreated)
                .WithOne(tc => tc.Creator)
                .HasForeignKey(tc => tc.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Member>().HasMany(m => m.TasksAssigned)
                .WithOne(ta => ta.Assigned)
                .HasForeignKey(ta => ta.AssingedId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Member>().HasMany(m => m.Comments)
                .WithOne(c => c.Member)
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Member>().HasMany(m => m.Categories)
                .WithOne(c => c.Creator)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasMany(u => u.Boards)
                .WithOne(b => b.Owner)
                .HasForeignKey(b => b.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
