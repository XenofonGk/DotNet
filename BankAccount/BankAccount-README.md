# Bank Account Simulator

A console app simulating basic bank account operations.

## Features
- Create an account with a name and initial deposit
- Deposit and withdraw funds with validation
- View transaction history
- Input validation with `int.TryParse`

## Tech
- C# / .NET 10
- Console I/O, classes, encapsulation, `List<string>`

## Run
```bash
cd BankAccount
dotnet run
```

## Concepts Practiced
- OOP — classes, properties with `private set`, constructors
- Encapsulation — balance can only change through `Deposit`/`Withdraw`
- Input validation — rejects negative amounts and overdrafts
