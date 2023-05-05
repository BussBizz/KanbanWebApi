namespace KanbanWebApi.Models
{
    public class KanbanTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        // FK
        public int CatergoryId { get; set; }
        public int? CreatorId { get; set; }
        public int? AssingedId { get; set; }

        // Navigation prop
        public Category? Category { get; set; }
        public Member? Creator { get; set; }
        public Member? Assigned { get; set; }

        // Collection prop
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}
