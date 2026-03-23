// See https://aka.ms/new-console-template for more information

Console.WriteLine("Calculator");

// int num = Convert.ToInt32("45");

    Console.Write("Enter a number: ");
    double num1 = Convert.ToDouble(Console.ReadLine());
      Console.Write("Enter a second number: ");
    double num2 = Convert.ToDouble(Console.ReadLine());
    
    Console.WriteLine("+" + num1 + num2);
    Console.WriteLine("-" + num1 - num2);
    Console.WriteLine("*" + num1 * num2);
    Console.WriteLine("/" + num1 / num2);
