namespace KanbanWebApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? Deadline { get; set; }

        // FK
        public int BoardId { get; set;}
        public int? CreatorId { get; set; }

        // Navigation prop
        public Board? Board { get; set; }
        public Member? Creator { get; set; }

        // Collection prop
        public ICollection<KanbanTask> KanbanTasks { get; set; } = null!;
    }
}
