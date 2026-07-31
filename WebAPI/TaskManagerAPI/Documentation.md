# TaskManager API Documentation

## Overview
This project is a simple Task Management Web API built with .NET 10 and PostgreSQL. It demonstrates basic CRUD operations, database migrations, and containerization with Docker.

## Project Setup Steps

### Step 1: Initialize Project
```bash
dotnet new webapi -n TaskManagerApi
```

### Step 2: Define the Model
Created `Models/TodoTask.cs` with the following properties:
- `Id`: Primary Key
- `Title`: Task description (Required)
- `isCompleted`: Status boolean
- `CreatedAt`: Timestamp

### Step 3: Database Context & ORM
Created `Data/AppDbContext.cs` and installed required NuGet packages:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`

### Step 4: Database Configuration
Updated `appsettings.json` with the PostgreSQL connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=<your-local-password>"
}
```

### Step 5: Service Registration
Registered the `AppDbContext` in `Program.cs` to enable dependency injection:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Step 6: Database Migrations
Created and applied migrations to set up the PostgreSQL table:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
*Note: Migrations are also applied automatically at application startup.*

### Step 7: Controllers
Implemented `Controllers/TodoController.cs` with the following endpoints:
- `GET /api/todo`: Fetch all tasks
- `POST /api/todo`: Create a new task

## Running the Application

### Option 1: Docker Compose (Full Containerization)
1. Build and start both the API and Database:
   ```bash
   docker-compose up -d
   ```
2. The API will be available at `http://localhost:5095`.

### Option 2: Local Run with Docker Database
1. Start the PostgreSQL container:
   ```bash
   docker-compose up -d db
   ```
2. Run the application:
   ```bash
   dotnet run
   ```

## Testing the API

### Check if the app is running:
```bash
curl -i http://localhost:5095/api/todo
```

### Add a new task:
```bash
curl -X POST http://localhost:5095/api/todo \
     -H "Content-Type: application/json" \
     -d '{"title": "Finish Week 4", "isCompleted": false}'
```

### Get all tasks:
```bash
curl -s http://localhost:5095/api/todo
```
