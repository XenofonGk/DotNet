using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// The connection string comes from configuration — environment variables in
// Docker, user-secrets locally. It is never committed.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// OpenAPI document at /openapi/v1.json
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    // HTTPS redirection stays off in development so the container can be
    // reached over plain HTTP without a certificate.
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHttpsRedirection();
}

app.MapControllers();

// A liveness endpoint, so a container orchestrator can tell whether the app is
// up without hitting the database.
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

// Applies pending migrations at startup. Convenient for a containerised demo
// that has to come up from nothing on a clean machine; a production system
// would run migrations as a deliberate deploy step instead of on boot.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
