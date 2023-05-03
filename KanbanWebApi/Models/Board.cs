using System.Collections.ObjectModel;

namespace KanbanWebApi.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Titel { get; set; } = null!;
        public DateTime? DeadLine { get; set; }

        // Foreign Key
        public int OwnerId { get; set; }

        // Navigation prop
        public User Owner { get; set; } = null!;

        // Collection prop
        public ICollection<Category> Categories { get; set; } = null!;
        public ICollection<Member> Members { get; set; } = null!;
    }
}
