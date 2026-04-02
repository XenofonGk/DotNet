# TaskManager API

A simple Task Management Web API built with .NET 10 and PostgreSQL, fully containerized with Docker.

## 🚀 Getting Started

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed.

### Running the App
1. Clone the repository.
2. Open a terminal in the project root.
3. Run the following command:
   ```bash
   docker-compose up -d
   ```

The API will be available at: `http://localhost:5095/api/todo`

### Running the App Locally (Optional)
1. Ensure the database is running:
   ```bash
   docker-compose up -d db
   ```
2. Run the application:
   ```bash
   dotnet run
   ```
3. The API will be available at: `http://localhost:5095/api/todo`


## 🛠 Tech Stack
- **Framework:** .NET 10.0
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Containerization:** Docker & Docker Compose

## 📌 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/todo` | Retrieve all tasks |
| POST   | `/api/todo` | Create a new task |
| GET    | `/weatherforecast` | Sample weather endpoint |
| GET    | `/openapi/v1.json` | OpenAPI Specification |

## 🏗 Project Structure
- `Controllers/`: API endpoints logic.
- `Models/`: Database entities.
- `Data/`: Database context and migrations.
- `Dockerfile`: Instructions for building the API image.
- `docker-compose.yml`: Orchestrates the API and PostgreSQL database.

## 📝 Database Migrations
Migrations are applied automatically when the Docker container starts. No manual steps required!
