namespace KanbanWebApi.Models
{
    public class LoginDTO
    {
        public User User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
