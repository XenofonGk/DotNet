# TaskManager API

A simple Task Management Web API built with .NET 10 and PostgreSQL, fully containerized with Docker.

## 🚀 Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed.

### Running the App
```bash
cp .env.example .env      # then edit POSTGRES_PASSWORD
docker compose up -d --build
```

The API is then at `http://localhost:5095`, with a liveness probe at
`http://localhost:5095/health` and the OpenAPI document at
`http://localhost:5095/openapi/v1.json`.

Compose waits for PostgreSQL to report healthy before starting the API. Without
that gate the API can win the race, fail its migration against a database that
is not accepting connections yet, and then serve errors from a schema that was
never created.

Credentials come from `.env`, which is gitignored. Nothing in this repository
contains a real password.

### Running Locally Without Docker
```bash
docker compose up -d db          # database only
dotnet run
```

## 🛠 Tech Stack
- **Framework:** .NET 10.0
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Containerization:** Docker & Docker Compose

## 📌 API Endpoints

| Method | Endpoint | Success | Missing id | Invalid body |
|--------|----------|---------|-----------|--------------|
| GET    | `/api/todo`      | 200 | — | — |
| GET    | `/api/todo/{id}` | 200 | 404 | — |
| POST   | `/api/todo`      | 201 + `Location` | — | 400 |
| PUT    | `/api/todo/{id}` | 204 | 404 | 400 |
| DELETE | `/api/todo/{id}` | 204 | 404 | — |
| GET    | `/health`        | 200 | — | — |
| GET    | `/openapi/v1.json` | 200 | — | — |

Requests bind to DTOs (`Dtos/TodoRequests.cs`) rather than to the `ToDo` entity.
Binding to the entity would let a caller post its own `Id` or `CreatedAt` and have
EF Core accept them, so a request naming an existing `Id` could overwrite a row it
was never meant to touch. `PUT` likewise leaves `CreatedAt` alone — an update is
not a creation.

### Verified behaviour

Run against PostgreSQL 16, every verb exercised:

```
POST   /api/todo   {"title":"Write CRUD tests"}     -> 201  Location: /api/Todo/1
POST   /api/todo   {"title":""}                     -> 400
POST   /api/todo   {"id":9999,"createdAt":"1990.."} -> 201  server assigned id=3
                                                            and its own timestamp
GET    /api/todo                                    -> 200  3 items
GET    /api/todo/1                                  -> 200
GET    /api/todo/999999                             -> 404
PUT    /api/todo/1 {"title":"Updated","isCompleted":true}
                                                    -> 204  CreatedAt unchanged
PUT    /api/todo/999999                             -> 404
DELETE /api/todo/1                                  -> 204
DELETE /api/todo/1                                  -> 404
GET    /api/todo/1                                  -> 404
```

The third line is the one worth reading twice: the client supplied both an `Id`
and a `CreatedAt`, and the server discarded both.

## 🏗 Project Structure
- `Controllers/`: API endpoint logic.
- `Dtos/`: request shapes, kept separate from the entities.
- `Models/`: Database entities.
- `Data/`: Database context and migrations.
- `Dockerfile`: Instructions for building the API image.
- `docker-compose.yml`: Orchestrates the API and PostgreSQL database.

## 📝 Database Migrations
Migrations are applied automatically when the Docker container starts. No manual steps required!
