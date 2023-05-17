namespace KanbanWebApi.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string Hash { get; set; } = null!;
        public DateTime Expire { get; set; }
    }

    public class BearerToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
    }
}
