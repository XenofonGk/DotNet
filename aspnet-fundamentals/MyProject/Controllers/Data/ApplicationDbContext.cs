using MyProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Dynamic;

namespace MyProject.Data;

/// <summary>
/// This class serves as the bridge between the application's C# models and the SQL database.
/// Inheriting from DbContext enables Entity Framework Core to perform all database operations (CRUD).
/// </summary>
public class ApplicationDbContext : DbContext
{
    // The constructor receives configuration settings (like connection strings) 
    // from Program.cs and passes them to the base DbContext class for processing.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // A DbSet represents a table in the database.
    // Entity Framework will automatically create a 'Categories' table based on the 'Category' model.
    // This property allows us to query and save data for categories.
    public DbSet<Category> Categories { get; set; }

    // another representation of a table in the database
    // Entity Framework will create a Supplier table based on the Supplier model
    // it allows us to query and save data for Suppliers
    public DbSet<Supplier> Supplier { get; set; }
}
