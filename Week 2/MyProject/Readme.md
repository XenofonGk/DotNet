# MyProject — ASP.NET Core MVC CRUD App

A full-stack web application built with ASP.NET Core MVC as part of my internship preparation roadmap for Mercell (Copenhagen, June 2026).

---

## What it does

Manages **Categories** and **Suppliers** with full CRUD operations — create, read, update, and delete — backed by a SQL Server database running in Docker.

---

## Tech Stack

- **ASP.NET Core MVC** (.NET 10)
- **Entity Framework Core** — ORM + migrations
- **SQL Server** — running in Docker
- **Razor Views** — server-side rendering with Tag Helpers
- **Bootstrap 5** — UI styling
- **Toastr** — toast notifications

---

## Features

- Full CRUD for Categories and Suppliers
- Server-side + client-side validation with data annotations
- TempData + Toastr notifications on create, edit, delete
- ViewModel pattern for strongly-typed views
- EF Core migrations for database schema management
- Dependency injection for DbContext

---

## Project Structure

```
MyProject/
├── Controllers/
│   ├── CategoryController.cs
│   ├── SupplierController.cs
│   └── Data/
│       └── ApplicationDbContext.cs
├── Models/
│   ├── Category.cs
│   └── Supplier.cs
├── ViewModels/
│   └── CategoryVM.cs
├── Views/
│   ├── Category/         ← Index, Create, Edit, Delete
│   ├── Supplier/         ← Index, Create, Edit, Delete
│   └── Shared/
│       └── _Layout.cshtml
├── Migrations/
├── appsettings.json
└── Program.cs
```

---

## Getting Started

### Prerequisites
- .NET 10 SDK
- Docker Desktop

### 1. Start SQL Server in Docker
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPass123!" \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Update connection string
Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=MyDb;User Id=sa;Password=YourPass123!;TrustServerCertificate=True;"
}
```

### 3. Apply migrations
```bash
dotnet ef database update
```

### 4. Run the app
```bash
dotnet watch run
```

Navigate to `https://localhost:{port}/Category`

---

## Concepts Covered

- MVC architecture — Models, Views, Controllers separation
- Dependency injection — DbContext registered in Program.cs
- EF Core — DbContext, DbSet, migrations, CRUD operations
- Data annotations — `[Key]`, `[Required]`, `[Range]`, `[DisplayName]`
- Tag Helpers — `asp-for`, `asp-action`, `asp-route-id`, `asp-validation-for`
- ViewModel pattern — strongly typed views combining multiple data sources
- TempData — passing messages across redirects
- Convention routing vs attribute routing
- Server-side + client-side validation

---

## Author

Xenofon Gkioka
2nd year Computer Programming & Analysis — Seneca Polytechnic
Incoming SWE Intern @ Mercell, Copenhagen (June 2026)
[Portfolio](https://XenofonGk.github.io)
