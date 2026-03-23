// See https://aka.ms/new-console-template for more information
namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
          
        char grade = 'A'; // char take only 1 character
        string name = "Xenofon";
        int age = 27;
        double gpa = 3.5;
        // string new = " ";
        bool isMale = true;
Console.WriteLine("========== Welcome ==============");
            Console.WriteLine("Hello, This is my first project on C#!");
            Console.WriteLine("My Name is " + name + " & i'm " + age +"years old" );
            Console.WriteLine("My grade is "+ grade + " and my gpa is " + gpa);
            // Console.ReadLine(new);
            // Console.WriteLine(new); 
Console.WriteLine("========== Same as js ==============");          
            Console.WriteLine(name.Length);
            Console.WriteLine(name.Contains("X"));
            Console.WriteLine(name[0]);
            Console.WriteLine(name.Substring(1)); // with ,3 or whatever num we can grab 3 chars
Console.WriteLine("========== Math ==============");
            Console.WriteLine( 2+ 2 * 2 / 2 % 2);
            Console.WriteLine ( 5 / 2.0); // int int = int  int double = double
            // ++ -- SAME 
            // call methods same as js
            Console.WriteLine(Math.Abs(-40));
Console.WriteLine("========== User Input ==============");
            Console.Write("Please enter your name: ");
            string newname = Console.ReadLine();
            Console.Write("Please enter your age");
            string newage = Console.ReadLine();
            Console.WriteLine("Hello " + newname + " you are: " + newage);
            
        }
    }
}

