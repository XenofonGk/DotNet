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