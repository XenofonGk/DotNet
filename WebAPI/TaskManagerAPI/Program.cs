using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Behind a reverse proxy, so the original scheme and client IP arrive as
// X-Forwarded-* headers and must be honoured for links and logging to be right.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // The proxy runs on the same host and is not in a known network range.
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// The connection string comes from configuration — environment variables in
// Docker, user-secrets locally. It is never committed.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// The portfolio calls this API from a different origin, so without CORS the
// browser blocks every response before the page can read it. Origins come from
// configuration rather than a wildcard: "*" cannot be used with credentials and
// gives away more than is needed.
const string PortfolioCors = "portfolio";
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
                     ?? new[] { "https://xenofongk.github.io", "http://localhost:4173" };

builder.Services.AddCors(options =>
{
    options.AddPolicy(PortfolioCors, policy => policy
        .WithOrigins(allowedOrigins)
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .WithHeaders("Content-Type"));
});

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
    // TLS is terminated by the reverse proxy in front of this container, which
    // forwards plain HTTP on the internal network. Redirecting here would send
    // clients into a loop.
    app.UseForwardedHeaders();
}

app.UseCors(PortfolioCors);

app.MapControllers();

// A liveness endpoint, so a container orchestrator can tell whether the app is
// up without hitting the database.
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

// Applies pending migrations at startup. Convenient for a containerised demo
// that has to come up from nothing on a clean machine; a production system
// would run migrations as a deliberate deploy step instead of on boot.
//
// Retried, because the database is not always ready when this process is: a
// serverless Postgres that has scaled to zero takes a moment to wake, and a
// compose stack can start both services at once.
//
// A failure here is fatal on purpose. Logging and carrying on left the app
// serving errors from a schema that was never created, which looks like a
// healthy container and is far harder to diagnose than one that refuses to
// start.
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<AppDbContext>();

    const int maxAttempts = 10;

    for (var attempt = 1; ; attempt++)
    {
        try
        {
            context.Database.Migrate();
            logger.LogInformation("Database migrations applied.");
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts)
        {
            // 2s, 4s, 6s … capped, so a cold database gets ~2 minutes to wake.
            var delay = TimeSpan.FromSeconds(Math.Min(attempt * 2, 15));
            logger.LogWarning(
                ex,
                "Migration attempt {Attempt}/{Max} failed; retrying in {Delay}s.",
                attempt, maxAttempts, delay.TotalSeconds);
            Thread.Sleep(delay);
        }
    }
}

app.Run();
