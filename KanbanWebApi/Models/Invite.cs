namespace KanbanWebApi.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public DateTime? Expire { get; set; }

        // FK
        public int? UserId { get; set; }
        public int BoardId { get; set; }

        // Navigation prop
        public User? User { get; set; }
        public Board? Board { get; set; }
    }
}
