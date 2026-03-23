# ASP.NET Core MVC - Complete Documentation
Xenofon Gkioka | Internship Prep 2026

---

## Project Structure
```
MyProject/
├── Controllers/
│   ├── CategoryController.cs        ← handles all category requests
│   ├── SupplierController.cs        ← handles all supplier requests
│   └── Data/
│       └── ApplicationDbContext.cs  ← bridge to database
├── Models/
│   ├── Category.cs                  ← Category data shape / DB table
│   └── Supplier.cs                  ← Supplier data shape / DB table
├── Views/
│   ├── Category/
│   │   ├── Index.cshtml             ← list all categories
│   │   ├── Create.cshtml            ← create form
│   │   ├── Edit.cshtml              ← edit form
│   │   └── Delete.cshtml            ← delete confirmation
│   ├── Supplier/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       └── _Layout.cshtml           ← master layout (navbar, footer, scripts)
├── Migrations/                      ← auto-generated DB sync files
├── appsettings.json                 ← connection string config
├── Program.cs                       ← app setup + DI container
└── wwwroot/                         ← static files (CSS, JS, images)
```

---

## MVC Architecture
Separated the app into three layers, each with one clear responsibility:
1. **Models** — data shape, maps to DB tables
2. **Controllers** — logic, receives requests, talks to DB, returns views
3. **Razor Views** — UI, renders HTML using C# + HTML mixed syntax

**Request flow:**
```
User types /Category/Index
→ Router maps to CategoryController.Index()
→ Controller calls _db.Categories.ToList()
→ Controller passes data to View via return View(data)
→ View renders HTML and sends back to browser
```

---

## SQL Server Setup (Docker)
Run SQL Server locally without installing it:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPass123!" \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Key points:
- Port `1433` is the default SQL Server port
- `SA_PASSWORD` must match exactly what's in your connection string
- Run `docker ps` to verify it's running before starting the app
- `TrustServerCertificate=True` in connection string is required for local Docker SQL Server

---

## Connection String (appsettings.json)
Tells EF Core where to find the database. Stored separately from code:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=MyDb;User Id=sa;Password=YourPass123!;TrustServerCertificate=True;"
}
```

---

## Program.cs — App Setup
Registers all services and middleware before the app runs:
```csharp
// Register MVC + Razor runtime compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register DbContext with DI + connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Middleware pipeline — order matters
app.UseHttpsRedirection();   // redirect HTTP to HTTPS
app.UseRouting();            // match URLs to controllers
app.UseAuthorization();      // enforce access control
app.MapStaticAssets();       // serve CSS/JS/images from wwwroot

// Default route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

---

## Models + Data Annotations
C# classes with properties that map directly to database columns:
```csharp
public class Category
{
    [Key]                          // Primary Key
    public int Id { get; set; }

    [Required]                     // Cannot be null/empty
    public string? Name { get; set; }

    [Range(1, 100)]                // Must be between 1 and 100
    public int DisplayOrder { get; set; }

    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
```

| Annotation | What it does |
|-----------|-------------|
| `[Key]` | Marks the primary key |
| `[Required]` | Field cannot be null |
| `[Range(min, max)]` | Validates numeric range |
| `[MaxLength(n)]` | Limits string length |
| `[DisplayName("X")]` | Label shown in UI instead of property name |

---

## ApplicationDbContext
Bridge between C# and SQL Server:
```csharp
public class ApplicationDbContext : DbContext
{
    // Constructor receives config from DI (connection string etc.)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Each DbSet = one table in the database
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}
```

---

## Dependency Injection
You don't create objects with `new` — you ask for them and ASP.NET provides them.

```csharp
// 1. Register in Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(...);

// 2. Inject in controller constructor
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db; // ASP.NET creates and passes this automatically
    }
}
```

**Why:**
- One place to configure (`Program.cs`)
- ASP.NET manages object lifetime — no memory leaks
- Easy to swap out for testing (fake DB instead of real one)
- Never call `new ApplicationDbContext()` manually

---

## Migrations
Keeps C# models and the database in sync:

| Command | What it does |
|---------|-------------|
| `dotnet ef migrations add <Name>` | Generates migration file describing SQL changes |
| `dotnet ef database update` | Executes the SQL against the database |

Inside every migration file:
- `Up()` → applies the change (CREATE TABLE, ADD COLUMN)
- `Down()` → rolls it back (DROP TABLE, DROP COLUMN)

**Run after every model change.**

---

## Full CRUD Controller
```csharp
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ — GET /Category/Index
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // CREATE — GET /Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // CREATE — POST /Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // EDIT — GET /Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var category = _db.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // EDIT — POST /Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // DELETE — GET /Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _db.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // DELETE — POST /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _db.Categories.Find(id);
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
```

**Why GET + POST for same action:**
- GET = show the user something (empty form, pre-filled form, confirmation page)
- POST = do something with submitted data (save, update, delete)
- Never modify data on a GET request

**Why `[ActionName("Delete")]` on DeletePOST:**
- C# can't have two methods with the same name and same parameter types
- `ActionName` tells ASP.NET the route is still "Delete" but C# method name is different

---

## EF Core Quick Reference
```csharp
_db.Categories.ToList()        // SELECT all
_db.Categories.Find(id)        // SELECT by primary key
_db.Categories.Add(obj)        // INSERT (stage)
_db.Categories.Update(obj)     // UPDATE (stage)
_db.Categories.Remove(obj)     // DELETE (stage)
_db.SaveChanges()              // COMMIT — always call after staging changes
```

---

## Razor Views + Tag Helpers
```cshtml
@model Category                          // declare single model
@model IEnumerable<Category>             // declare list model
@item.Name                               // output property value
@foreach(var item in Model) { }          // loop through list
@{ ViewData["Title"] = "Page Title"; }   // set browser tab title
@* this is a Razor comment *@            // comment (not sent to browser)
```

| Tag Helper | What it generates |
|-----------|------------------|
| `asp-controller="Category"` | sets controller part of URL |
| `asp-action="Index"` | sets action part of URL |
| `asp-route-id="@item.Id"` | appends id to URL: `/Category/Edit/5` |
| `asp-for="Name"` | wires input to model property + validation |
| `asp-validation-for="Name"` | shows field-level validation error |
| `asp-validation-summary="All"` | shows all validation errors at once |

---

## Server-Side Validation
Two things needed in every Create/Edit view:

**1. Summary — at top of form:**
```cshtml
<div asp-validation-summary="All" class="text-danger"></div>
```

**2. Field-level errors — under each input:**
```cshtml
<input asp-for="Name" class="form-control"/>
<span asp-validation-for="Name" class="text-danger"></span>
```

**3. Scripts — at bottom of view:**
```cshtml
@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
```

Validation flow:
- `[Required]` on model → EF Core enforces in DB
- `ModelState.IsValid` in controller → checks annotations before saving
- `asp-validation-summary` + `asp-validation-for` in view → shows errors to user
- Scripts partial → enables client-side validation (before form submits)

---

## TempData
Passes messages across redirects — survives exactly one redirect then is cleared.

**In controller (before redirect):**
```csharp
TempData["success"] = "Category created successfully";
return RedirectToAction("Index");
```

**Why not a regular variable:**
After `RedirectToAction`, the controller dies and a brand new HTTP request starts.
Regular variables are gone. TempData is stored in session temporarily.

---

## Toastr Notifications
Cleaner than Bootstrap alert divs — shows as a popup that auto-dismisses.

**1. Add CSS to `<head>` in `_Layout.cshtml`:**
```html
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css"/>
```

**2. Add JS + TempData check at bottom of `_Layout.cshtml` (order matters):**
```cshtml
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
@if (TempData["success"] != null)
{
    <script>
        toastr.success('@TempData["success"]');
    </script>
}
@await RenderSectionAsync("Scripts", required: false)
```

**Script load order — jQuery → Bootstrap → Toastr → Toastr call**
Toastr depends on jQuery. If Toastr loads before jQuery, it breaks silently.

---

## Common Mistakes
| Mistake | Fix |
|---------|-----|
| Forgot `_db.SaveChanges()` | Data staged but never saved to DB |
| `DeletePOST` name conflict | Use `[ActionName("Delete")]` on POST method |
| Variable declared inside `if` block | Declare before the if so it's in scope |
| Wrong namespace casing | C# is case sensitive — `MyProject` ≠ `Myproject` |
| `type="delete"` on button | Always `type="submit"` |
| Toastr not showing | Check script load order — jQuery must load first |
| TempData key mismatch | `TempData["success"]` ≠ `TempData["Success"]` |
| Migration not run after model change | Always run `dotnet ef migrations add` + `update` |
