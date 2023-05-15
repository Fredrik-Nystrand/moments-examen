namespace backend.Models.Chore.CompletedChore
{
    public class CompletedChoreResponse
    {
        public int Id { get; set; }
        public int CompletedXp { get; set; }
        public DateTime DateCompleted { get; set; }
        public int ChoreId { get; set; }
        public string ChoreName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
