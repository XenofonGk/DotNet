using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyProject.Models;

/// <summary>
/// This class represents a 'Supplier' entity in the database.
/// Each property corresponds to a column in the 'Suppliers' table.
/// </summary>


public class Supplier
{
    // Primary key 
    [Key]
    public int? Id { get; set; }

    // Required field, cant be empty 
    [Required]
    public string? Name { get; set; }

    // Defines Contac email also required
    [Required]
    [DisplayName("Contact Email")]
    public string? ContactEmail { get; set; }

    // Defines the country 
    public string Country { get; set; }

    // Stores the date and time when the category was created.
    // Defaults to the current server time (DateTime.Now).
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}