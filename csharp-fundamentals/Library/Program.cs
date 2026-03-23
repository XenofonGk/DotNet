using System.Diagnostics;

List<Book> books = new List<Book>
{
    new Book("The Pragmatic Programmer", "David Thomas", 352),
    new Book("Clean Code", "Robert Martin", 431),
    new Book("The Hobbit", "J.R.R. Tolkien", 310),
    new Book("1984", "George Orwell", 328)
};

Console.WriteLine("Welcome to my Library!");

bool running = true;
while (running) {
Console.WriteLine("=====================");
Console.WriteLine("1. Borrow a Book");
Console.WriteLine("2. Return a Book");
Console.WriteLine("3. View all Books");
Console.WriteLine("4. Exit");
Console.WriteLine("=====================\n");


string choice = Console.ReadLine() ?? "";



    switch (choice)
    {
        case "1":
        if (books.All(b => b.IsBorrowed))
            {
                Console.WriteLine("All books are currently borrowed");
                break;
            }
    for (int i = 0; i < books.Count; i++)
            {
                string status = books[i].IsBorrowed ? "Borrowed" : "Available";
                Console.WriteLine($"{i+1}, {books[i].Title} - {books[i].Author} - {status} - {books[i].DueDate}");
            }
            Console.WriteLine("Pick a book");
            string input1 = Console.ReadLine() ?? "";
            if (!int.TryParse(input1, out int index))
            {
                Console.WriteLine("Please enter a valid number");
                break;
            }
                 else {
                                index = index - 1;

                if (books[index].BorrowBook())
    Console.WriteLine("Book borrowed successfully");
else
    Console.WriteLine("Book is already borrowed");
            }
            break;
        case "2":
          if (!books.Any(b => b.IsBorrowed))
            {
                Console.WriteLine("No books are currently borrowed");
                break;
            }
        for (int i = 0; i < books.Count; i++)
            {
                string status = books[i].IsBorrowed ? "Borrowed" : "Available";
                Console.WriteLine($"{i+1}, {books[i].Title} - {books[i].Author} - {status} - {books[i].DueDate}");
            }
             Console.WriteLine("Which book are you returning today?");
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int indexx))
            {
                Console.WriteLine("Please enter a valid number");
                break;
            }
             else {
                            indexx = indexx - 1; 

                if (books[indexx].ReturnBook())
    Console.WriteLine("Book Returned successfully");
else
    Console.WriteLine("Book is already Returned");
            }
            break;
         case "3":
    for (int i = 0; i < books.Count; i++)
    {
        string status = books[i].IsBorrowed ? "Borrowed" : "Available";
        Console.WriteLine($"{i+1}. {books[i].Title} - {books[i].Author} - {status} -  {books[i].DueDate}");
    }
    break;
        case "4":
            running = false;
            break;
    }

}

