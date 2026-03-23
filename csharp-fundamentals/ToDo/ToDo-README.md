# To-Do List

A console app for managing tasks.

## Features
- Add tasks
- Mark tasks as done
- Delete tasks
- View all tasks with status (Pending / Done)
- Validates empty input and out-of-range selections

## Tech
- C# / .NET 10
- Console I/O, `List<TodoItem>`, `int.TryParse`

## Run
```bash
cd ToDo
dotnet run
```

## Concepts Practiced
- Separation of concerns — `TodoItem` class handles state, `Program.cs` handles UI
- `int.TryParse` for safe input parsing
- `List<T>` — Add, RemoveAt, Count
