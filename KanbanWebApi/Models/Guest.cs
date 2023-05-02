namespace KanbanWebApi.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public bool CanComplete { get; set; }
        public bool CanCreate { get; set; }
        public bool CanAssign { get; set; }

        // FK
        public int KanbanBoardId { get; set; }
        public int UserId { get; set; }

        // FK Relation prop
        public KanbanBoard KanbanBoard { get; set; }
        public User User { get; set; }
    }
}
