namespace KanbanWebApi.Models
{
    public class Member
    {
        public int Id { get; set; }
        public bool CanComplete { get; set; } = true;
        public bool CanCreate { get; set; } = false;
        public bool CanAssign { get; set; } = false;

        // FK
        public int BoardId { get; set; }
        public int UserId { get; set; }

        // Navigation prop
        public Board Board { get; set; } = null!;
        public User User { get; set; } = null!;

        // Collection prop
        public ICollection<KanbanTask> TasksAssigned { get; set;} = null!;
        public ICollection<KanbanTask> TasksCreated { get; set;} = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
        public ICollection<Category> Categories { get; set; } = null!;
    }
}
