namespace backend.Models.Chore;

public class ChoreRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int BaseXp { get; set; }
    public bool IsTimebased { get; set; }
    public int ChoreCategoryId { get; set; }
}
