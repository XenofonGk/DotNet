
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

class Book
    {
       public string Title { get; private set; }
       public string Author { get; private set; }
       public int Pages { get; private set; } 

       public bool IsBorrowed { get; private set; }

       public DateTime? DueDate { get; private set; }
            // ? means it can be null
    public Book(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
        IsBorrowed = false;
        DueDate = null;
    }

   public bool BorrowBook()
    {
        if (IsBorrowed)
        {
            return false;
        }
        IsBorrowed = true;
        DueDate = DateTime.Now.AddDays(14);

        return true;
    }

    public bool ReturnBook()
    {
        if (!IsBorrowed)
        {
            return false;
        }
        IsBorrowed = false;
        DueDate = null;
        return true;
    }

    }
