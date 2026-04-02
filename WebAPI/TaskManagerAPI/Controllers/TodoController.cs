using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI;

namespace TaskManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor Injecting the DB bridge
    public TodoController(AppDbContext context)
    {
        _context = context;
    }

// Get: api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }
// Post apo/todo
    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateTask(ToDo task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return Ok(task);
    }
}