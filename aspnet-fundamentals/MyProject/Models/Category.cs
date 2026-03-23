using System.ComponentModel.DataAnnotations;

namespace MyProject.Models;

/// <summary>
/// This class represents a 'Category' entity in the database.
/// Each property corresponds to a column in the 'Categories' table.
/// </summary>
public class Category
{
    // [Key] marks this property as the Primary Key (Unique ID) in the database.
    [Key]
    public int Id { get; set; }

    // [Required] ensures that this field cannot be empty (Null) in the database.
    [Required]
    public string Name { get; set; }
    
    // Defines the order in which the category will be displayed in the UI.
    public int DisplayOrder { get; set; }

    // Stores the date and time when the category was created.
    // Defaults to the current server time (DateTime.Now).
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
