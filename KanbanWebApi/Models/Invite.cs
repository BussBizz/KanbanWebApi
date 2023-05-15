namespace KanbanWebApi.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;

        // FK
        public int? UserId { get; set; }
        public string BoardId { get; set; } = null!;

        // Navigation prop
        public User? User { get; set; }
        public Board Board { get; set; } = null!;
    }
}
