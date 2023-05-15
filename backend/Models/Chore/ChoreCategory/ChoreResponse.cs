namespace backend.Models.Chore.ChoreCategory
{
    public class ChoreResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BaseXp { get; set; }
        public bool IsTimebased { get; set; }
        public int ChoreCategoryId { get; set; }
        public string ChoreCategoryName { get; set; }
    }
}
