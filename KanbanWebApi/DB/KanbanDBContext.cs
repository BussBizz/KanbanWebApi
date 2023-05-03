using KanbanWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.DB
{
    public class KanbanDBContext : DbContext
    {
        public KanbanDBContext(DbContextOptions<KanbanDBContext> options) : base(options)
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<KanbanBoard> KanbanBoards { get; set; }
        public DbSet<KanbanCategory> KanbanCategories { get; set; }
        public DbSet<KanbanComment> KanbanComments { get; set; }
        public DbSet<KanbanTask> KanbanTasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
