using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Dtos;

/// <summary>
///     Request shapes for creating and updating a task.
///
///     These exist so the controller never binds straight to the ToDo entity.
///     Binding to the entity lets a caller post its own Id or CreatedAt and have
///     EF Core accept them — the server owns both of those values, and Id in
///     particular decides which row gets written.
/// </summary>
public record CreateTodoRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    public bool IsCompleted { get; init; }
}

public record UpdateTodoRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    public bool IsCompleted { get; init; }
}
