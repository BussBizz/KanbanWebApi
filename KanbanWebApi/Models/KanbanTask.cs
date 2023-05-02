using System.ComponentModel.DataAnnotations;

namespace KanbanWebApi.Models
{
    public class KanbanTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        // FK
        public int CatergoryId { get; set; }
        public int CreatorId { get; set; }
        public int? AssingedId { get; set; }

        // FK
        public KanbanCategory KanbanCategory { get; set; }
        public User Creator { get; set; }
        public User? Assigned { get; set; }
        
    }
}
