// See https://aka.ms/new-console-template for more information
using System.Collections;
Console.WriteLine("Welcome to my bank");

Console.WriteLine("First of all we have to create a bank account");

Console.WriteLine("Enter your name: ");
string name = Console.ReadLine() ?? "";
Console.WriteLine("Enter Initial Amount ");
decimal initialDeposit = Convert.ToDecimal(Console.ReadLine());

bool running = true;
BankAccount account = new BankAccount(name, initialDeposit);

while (running) {
Console.WriteLine("1. Deposit");
Console.WriteLine("2. Withdraw");
Console.WriteLine("3. View Transaction History");
Console.WriteLine("4. Exit\n");

string choice = Console.ReadLine() ?? "";



    switch (choice)
    {
        case "1":
            Console.WriteLine("Please enter an amount: ");
            decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
            account.Deposit(depositAmount);
            break;
        case "2":
            Console.WriteLine("Please enter an amount: ");
            decimal WithdrawAmount = Convert.ToDecimal(Console.ReadLine());
            account.Withdraw(WithdrawAmount);
            break;
        case "3":
            account.PrintHistory();
            break;
        case "4":
            running = false;
            break;
    }

}