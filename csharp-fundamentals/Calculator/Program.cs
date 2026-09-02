// A four-function calculator over two numbers read from the console.

Console.WriteLine("Calculator");

double num1 = ReadNumber("Enter a number: ");
double num2 = ReadNumber("Enter a second number: ");

// Each result is computed first and then formatted. Writing
// Console.WriteLine("+" + num1 + num2) instead concatenates the operands
// onto the sign and prints "+53" for 5 and 3 rather than 8 — and the
// subtraction form does not compile at all, because a string cannot be
// subtracted from.
Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
Console.WriteLine($"{num1} * {num2} = {num1 * num2}");

if (num2 == 0)
{
    Console.WriteLine($"{num1} / {num2} = undefined (cannot divide by zero)");
}
else
{
    Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
}

// Convert.ToDouble throws on anything non-numeric, which ends the program
// on a typo. TryParse lets the user correct it instead.
static double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (double.TryParse(Console.ReadLine(), out double value))
        {
            return value;
        }
        Console.WriteLine("That is not a number. Try again.");
    }
}
