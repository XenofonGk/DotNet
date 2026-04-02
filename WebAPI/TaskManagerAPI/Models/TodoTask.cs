
using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Data;
public class ToDo
/// <summary>
///     This class repserent a WebAPI entity in the database
///     Each property corresponds to a column in the WebAPI table
/// </summary> 
{
    // [key] Marks this property as a PRIMARY KEY
    [Key]
    public int Id { get; set; }
    // [Required] ensueres that this field cannot be empty in the DB
    [Required]
    public string Title { get; set; } = string.Empty;
    // Defines true or false 
    public bool isCompleted { get; set; }
    // Defines the date and time each task its created 
    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;
}
