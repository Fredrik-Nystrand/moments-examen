using backend.Models.Chore;
using backend.Models.Chore.ChoreCategory;
using backend.Models.Chore.CompletedChore;
using backend.Models.User;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ChoreEntity> Chores { get; set; }
    public DbSet<ChoreCategoryEntity> ChoreCategories { get; set; }
    public DbSet<CompletedChoreEntity> CompletedChores { get; set; }
}
