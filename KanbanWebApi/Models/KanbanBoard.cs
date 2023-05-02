namespace KanbanWebApi.Models
{
    public class KanbanBoard
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public DateTime DeadLine { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // FK Relation prop
        public User User { get; set; }
    }
}
