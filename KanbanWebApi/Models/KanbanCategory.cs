namespace KanbanWebApi.Models
{
    public class KanbanCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }

        // FK
        public int KanbanBoardId { get; set;}
        public int CreatorId { get; set; }

        // FK Relation prop
        public KanbanBoard KanbanBoard { get; set; }
        public User Creator { get; set; }
    }
}
