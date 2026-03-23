I learned 

- MVC Architecture
    Seperated the app into thre layers, each part has one clear responsibility:
        1) Models for data controllers
        2) Controllers for logic
        3) Razor views for the UI            
- Models + Data Annotations
    Created C# classes with properties that map to database columns using [Key] & [Required] to add contraints to app and db
- DbContext + DbSet
    Wired up ApplicationDbContext inheriting from DbContext with a DbSet per table
    Giving EF Core the blueprint to query and modify the DB
- Migrations
    Used [dotnet ef migrations add] to snapshot model changes
         [dotnet ef database update] to apply them as SQL keeping C# and the DB in sync
- Full CRUD Controller
    Built: GET & POST actions for Create, Edit , Delete and Index handling model validation
    Db operateions and redirects after each action
- Razor views with Tag Helpers
    Wrote .cshtml views using asp-for, asp-action and asp-route-id to dynamically generate forms and links
    tied directly to controller actions.
- Dependency Injection
    Registered ApplicationDbContext in Program.cs and injected it into controllers via the constructor
    letting ASP.NET manage the object lifetime instead of manually calling new.
- VieWBag vs ViewData vs ViewModel
    1) ViewData: is a dictionary — you store anything with a string key and retrieve it by that same key. The limitation is it's not typed — you store it as object and the view has no idea what's inside without casting.
        @{ ViewData["Title"] = "Category List"; }
    2) viewBag: is the same thing but with dynamic syntax sugar
        // Controller
            ViewBag.Message = "Hello";
        // View
            @ViewBag.Message      

        -   Both have the same problem — no IntelliSense, no type checking, typo in the key = runtime error not compile error.  
    3) ViewModel solves this — a dedicated C# class that carries exactly the data the view needs:
        // ViewModel class
            public class CategoryViewModel
        {
            public List<Category> Categories { get; set; }
            public string PageTitle { get; set; }
        }

        // Controller
            var vm = new CategoryViewModel {
            Categories = _db.Categories.ToList(),
            PageTitle = "All Categories"
        };
            return View(vm);

        // View
        @model CategoryViewModel
        @Model.PageTitle
        @Model.Categories

-Routing
    Convention routing — ASP.NET maps URL automatically from controller/action name: /Category/Index → CategoryController.Index(). Defined once in Program.cs. Good for MVC apps.
    Attribute routing — custom URL defined directly on the action with [Route("custom/url")]. Good for APIs and custom URL structures. Used in Week 3 when building API endpoints.