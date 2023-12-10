using Lab2.exceptions;
using Task = Lab2.entities.Task;
using TodoList = Lab2.services.TodoList;

namespace Lab2;

internal static class Program
{
    private static void Main()
    {
        var todoList = new TodoList();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Search task");
            Console.WriteLine("3. Last tasks");
            Console.WriteLine("4. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask(todoList);
                    break;
                case "2":
                    SearchTask(todoList);
                    break;
                case "3":
                    LastTasks(todoList);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    private static void AddTask(TodoList todoList)
    {
        Console.WriteLine("New task");
        
        Console.Write("Theme: ");
        var theme = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(theme)) throw new TaskException("Empty theme");
        
        Console.Write("Description: ");
        var description = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(description)) throw new TaskException("Empty description");

        Console.Write("Deadline: ");
        var deadline = DateTime.Parse(Console.ReadLine() ?? throw new TaskException("Deadline can't be null"));

        var tags = new List<string?>();
        Console.WriteLine("Tags (finish on empty line)");
        var tagNumber = 1;
        
        while (true)
        {
            Console.Write($"{tagNumber}. ");
            var tag = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tag)) break;
            tags.Add(tag);
            tagNumber++;
        }
        
        todoList.Add(new Task(theme, description, deadline, tags));
    }

    private static void SearchTask(TodoList todoList)
    {
        Console.Write("Input tags for search: ");
        var searchTag = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(searchTag)) throw new TaskException("Tag can't be null");

        var result = todoList.SearchByTag(searchTag);

        if (result.Count == 0)
            Console.WriteLine("No such tasks");
        else
        {
            Console.WriteLine("Search results:");
            foreach (var task in result) Console.WriteLine(task);
        }
    }

    private static void LastTasks(TodoList todoList)
    {
        var result = todoList.GetCurrent();

        if (result.Count == 0)
        {
            Console.WriteLine("No current tasks.");
            return;
        }
        
        Console.WriteLine("Current tasks:");
        foreach (var task in result) Console.WriteLine(task);
    }
}