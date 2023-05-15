namespace backend.Models.User;

public class UserResponse
{

    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Xp { get; set; }
    public int Currency { get; set; }
    public int Level { get; set; }
}
