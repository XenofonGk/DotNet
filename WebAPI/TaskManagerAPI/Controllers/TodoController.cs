using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Controllers;

/// <summary>
///     CRUD endpoints over the ToDo entity.
///
///     Requests bind to DTOs rather than to the entity, so a caller cannot post
///     its own Id or CreatedAt — the server owns both.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor injecting the DB bridge
    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Returns every task, newest first.</summary>
    // GET: api/todo
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetTasks()
    {
        // AsNoTracking: these entities are serialised and discarded, so there is
        // no reason to pay for change tracking on a read.
        return await _context.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    /// <summary>Returns one task, or 404 when no task has that id.</summary>
    // GET: api/todo/5
    [HttpGet("{id:int}", Name = nameof(GetTask))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ToDo>> GetTask(int id)
    {
        var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    /// <summary>Creates a task and returns 201 with its location.</summary>
    // POST: api/todo
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ToDo>> CreateTask(CreateTodoRequest request)
    {
        var task = new ToDo
        {
            Title = request.Title,
            isCompleted = request.IsCompleted,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // 201 with a Location header rather than 200, so the client is told
        // where the new resource lives instead of having to construct the URL.
        return CreatedAtRoute(nameof(GetTask), new { id = task.Id }, task);
    }

    /// <summary>Replaces a task. 404 if absent, 204 on success.</summary>
    // PUT: api/todo/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTask(int id, UpdateTodoRequest request)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        task.Title = request.Title;
        task.isCompleted = request.IsCompleted;
        // CreatedAt is deliberately left alone: it records when the task was
        // created, and an update is not a creation.

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Deletes a task. 404 if absent, 204 on success.</summary>
    // DELETE: api/todo/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
