// See https://aka.ms/new-console-template for more information

Console.WriteLine("Welcome to my To-Do app!");


Console.WriteLine("First of all we have to enter a title");

Console.WriteLine("Enter a title: ");
string name = Console.ReadLine() ?? "";

bool running = true;
List<TodoItem> tasks = new List<TodoItem>(); 
while (running) {
Console.WriteLine("=====================");
Console.WriteLine("1. Add task");
Console.WriteLine("2. Mark as done");
Console.WriteLine("3. Delete Task");
Console.WriteLine("4. View Todo List");
Console.WriteLine("5. Exit");
Console.WriteLine("=====================\n");


string choice = Console.ReadLine() ?? "";



    switch (choice)
    {
        case "1":
        Console.WriteLine("Enter a tasks: ");
        string title = Console.ReadLine() ?? "";
        tasks.Add(new TodoItem(title));
            break;
        case "2":
            for (int i = 0; i < tasks.Count; i++)
         {
            Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].IsDone ? "Done" : "Pending")}");
         }
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
        tasks[index].MarkDone();
       
            break;
            case "3":
            for (int i = 0; i < tasks.Count; i++)
         {
            Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].IsDone ? "Done" : "Pending")}");
         }
            int indexx = Convert.ToInt32(Console.ReadLine()) - 1;
        tasks.RemoveAt(indexx);
            break;
        case "4":
            for (int i = 0; i < tasks.Count; i++)
         {
            Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].IsDone ? "Done" : "Pending")}\n");
            
         }
            break;
        case "5":
            running = false;
            break;
    }

}

