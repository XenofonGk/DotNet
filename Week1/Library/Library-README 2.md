# Library Book Manager

A console app for managing a library's book inventory.

## Features
- Borrow a book (sets due date 14 days from today)
- Return a book
- View all books with availability and due dates
- Prevents borrowing already-borrowed books
- Prevents returning books that aren't borrowed
- Input validation — rejects non-numeric input and out-of-range selections

## Tech
- C# / .NET 10
- `DateTime`, nullable types (`DateTime?`), LINQ, `int.TryParse`

## Run
```bash
cd Library
dotnet run
```

## Concepts Practiced
- `DateTime` and nullable value types
- LINQ — `All()`, `Any()` for collection state checks
- Boolean return values for operation success/failure
- Separating business logic from console output
