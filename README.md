# DotNet

A collection of .NET / C# projects, ranging from console exercises to a couple of small full-stack web applications. Everything targets **.NET 10**.

The repository is organized into three areas:

- [`WebAPI/TaskManagerAPI`](#webapitaskmanagerapi) — a containerized ASP.NET Core Web API backed by PostgreSQL.
- [`aspnet-fundamentals`](#aspnet-fundamentals) — ASP.NET Core learning projects, including an MVC CRUD application backed by SQL Server.
- [`csharp-fundamentals`](#csharp-fundamentals) — console-based C# exercises.

---

## Security note

None of the projects in this repository commit real connection strings, passwords, or other secrets. `appsettings.json` only holds non-secret configuration. Locally, connection strings are supplied through [.NET user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets); for the Dockerized API, they are supplied through a local `.env` file (see `.env.example`). In production, they would be supplied through environment variables or a secret manager. This is intentional — do not add real credentials to any `appsettings.json` or `docker-compose.yml` file in this repo.

---

## WebAPI/TaskManagerAPI

A small ASP.NET Core Web API for managing to-do tasks, fully containerized with Docker.

**What it does:** exposes a `Tasks` resource backed by PostgreSQL via Entity Framework Core. Currently implemented endpoints:

- `GET /api/todo` — list all tasks
- `POST /api/todo` — create a task

Database migrations are applied automatically on application startup (see `Program.cs`).

**Stack:** ASP.NET Core (.NET 10), Entity Framework Core, Npgsql (PostgreSQL provider), Docker & Docker Compose.

**How to run (Docker, recommended):**

```bash
cd WebAPI/TaskManagerAPI
cp .env.example .env      # then edit .env with your own local password
docker compose up -d
```

The API will be available at `http://localhost/api/todo`.

**How to run locally (without Docker):**

```bash
cd WebAPI/TaskManagerAPI

# Point the app at your own PostgreSQL instance:
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=<your-local-password>"

dotnet run
```

The API will be available at `http://localhost:5095/api/todo` (see `Properties/launchSettings.json`).

---

## aspnet-fundamentals

ASP.NET Core learning projects built while working through core web framework concepts.

### MyProject — ASP.NET Core MVC CRUD app

**What it does:** manages `Category` and `Supplier` records with full CRUD (create, read, update, delete) through Razor views, backed by SQL Server via Entity Framework Core. Includes server-side model validation and EF Core migrations for schema management.

**Stack:** ASP.NET Core MVC (.NET 10), Entity Framework Core (SQL Server provider), Razor Views, Bootstrap.

**How to run:**

```bash
cd aspnet-fundamentals/MyProject

# Requires a reachable SQL Server instance, e.g. via Docker:
# docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<your-local-password>" \
#   -p 1433:1433 --name sql-server -d mcr.microsoft.com/mssql/server:2022-latest

dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=MovieDb;User Id=sa;Password=<your-local-password>;TrustServerCertificate=True;"

dotnet ef database update
dotnet run
```

### TodoApi

A REST API following the official Microsoft "Create a web API" tutorial: full CRUD over todo items using an EF Core in-memory database (no external database required). See `aspnet-fundamentals/TodoApi/ReadMe.md` for endpoint details and run instructions.

---

## csharp-fundamentals

Standalone console exercises used to practice core C# and .NET fundamentals: `BankAccount` (with a companion test project), `Calculator`, `Library`, `MadLibs`, `StudentGrade`, `ToDo`, and `Week1App`. Each is a self-contained console app runnable with `dotnet run` from its own project folder.

---

## License

Released under the [MIT License](LICENSE).
