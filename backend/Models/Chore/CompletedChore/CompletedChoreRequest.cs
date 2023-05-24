namespace backend.Models.Chore.CompletedChore;

public class CompletedChoreRequest
{
    public string DateCompleted { get; set; } = String.Empty;
    public int ChoreId { get; set; }
    public int UserId { get; set; }
    public string? Amount { get; set; } = null;
}
