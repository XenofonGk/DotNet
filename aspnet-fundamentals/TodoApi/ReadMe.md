# TodoApi

A RESTful Web API built with ASP.NET Core following the official Microsoft tutorial. Supports full CRUD operations on todo items using an in-memory database and Entity Framework Core.

## Tech Stack

- .NET 9 / ASP.NET Core
- Entity Framework Core (InMemory)
- Swagger / OpenAPI (NSwag)
- C#

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todoitems` | Get all todo items |
| GET | `/api/todoitems/{id}` | Get a todo item by ID |
| POST | `/api/todoitems` | Create a new todo item |
| PUT | `/api/todoitems/{id}` | Update an existing todo item |
| DELETE | `/api/todoitems/{id}` | Delete a todo item |

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) with C# Dev Kit

### Run the app

```bash
dotnet run --launch-profile https
```

Then navigate to `https://localhost:{port}/swagger` to interact with the API via Swagger UI.

### Run the tests

```bash
dotnet test
```

## Project Structure

```
TodoApi/
├── Controllers/
│   └── TodoItemsController.cs   # API endpoints
├── Models/
│   ├── TodoItem.cs              # Data model
│   └── TodoContext.cs           # EF Core DbContext
├── Program.cs                   # App entry point and service registration
└── TodoApi.csproj
```

## Notes

- Uses an **in-memory database** — data does not persist between restarts.
- Built as part of a self-directed .NET learning roadmap in preparation for a software development internship.