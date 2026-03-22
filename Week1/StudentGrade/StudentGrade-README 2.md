# Student Grade Report

A console app that generates a grade report for a group of students.

## Features
- Calculates GPA from raw scores (0-100 → 0-4.0 scale)
- Determines pass/fail per student (GPA > 1.0 = pass)
- Prints full student report
- Ranks students by GPA using LINQ

## Tech
- C# / .NET 10
- LINQ — `Average()`, `OrderByDescending()`
- `string.Join()`, `foreach`

## Run
```bash
cd StudentGrade
dotnet run
```

## GPA Scale
| Score | GPA |
|-------|-----|
| 90-100 | 4.0 |
| 80-89 | 3.0 |
| 70-79 | 2.0 |
| 60-69 | 1.0 |
| Below 60 | 0.0 |

## Concepts Practiced
- Derived properties — GPA calculated once in constructor from grades array
- LINQ aggregation and sorting
- `foreach` vs `for` loop usage
