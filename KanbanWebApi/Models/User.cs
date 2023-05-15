using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAnon { get; set; } = true;

        // Collection prop
        public ICollection<Board> Boards { get; set; } = null!;
        public ICollection<Member> Memberships { get; set; } = null!;
    }
}
