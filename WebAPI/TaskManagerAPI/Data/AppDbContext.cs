using Microsoft.EntityFrameworkCore; 
using TaskManagerAPI;
namespace TaskManagerAPI.Data;

/// <summary> 
/// This class serves as the bridge between the app's C# models and the SQL DB
/// Inheriting from DbContext enables Entity Framework Core to perform all db operations(CRUD)
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) { }

    public DbSet<ToDo> Tasks { get; set; }
}