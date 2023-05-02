namespace KanbanWebApi.Models
{
    public class KanbanComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public byte[]? Image { get; set; }

        // FK
        public int TaskId { get; set; }
        public int UserId { get; set; }

        // FK Relation prop
        public KanbanTask Task { get; set; }
        public User User { get; set; }
    }
}
