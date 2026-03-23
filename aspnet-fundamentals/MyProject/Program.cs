using MyProject.Data; // IMPORT: Allows access to the ApplicationDbContext class
using Microsoft.EntityFrameworkCore; // IMPORT: Required for the .UseSqlServer() extension method
using System.Net.Security;
using Microsoft.VisualBasic;

// Create the builder for the web application
var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container.
// Controllers with views are added for MVC functionality.
// RazorRuntimeCompilation is included for real-time UI updates during development.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// CONFIGURATION: Database Context Setup
// This connects the 'ApplicationDbContext' class to the SQL Server database.
// 'options' is a parameter that allows us to configure how the context works.
// 'builder.Configuration.GetConnectionString("DefaultConnection")' reads the database URL from appsettings.json.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline (Middleware).
// This determines how incoming HTTP requests are processed.

// If the app is not in development mode, use a generic error handler and HSTS.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // HSTS (HTTP Strict Transport Security) adds security by enforcing HTTPS.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS for security.
app.UseHttpsRedirection();

// Enable routing for matching incoming requests to controllers and actions.
app.UseRouting();

// Authorization middleware ensures only authorized users can access certain resources.
// Note: UseAuthentication() would normally be placed before UseAuthorization().
app.UseAuthorization();

// Serve static assets (CSS, JS, images) from wwwroot.
app.MapStaticAssets();

// Define the default route pattern for the application.
// Default: Home controller, Index action, and an optional ID.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Run the application
app.Run();
