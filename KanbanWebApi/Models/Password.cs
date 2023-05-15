namespace KanbanWebApi.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Hash { get; set; } = null!;

        // FK
        public int UserId { get; set; }

        // Navigation Prop
        public User User { get; set; } = null!;
    }
}
