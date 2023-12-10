using Lab3.exceptions;
using System.Diagnostics.Metrics;
using Task = Lab3.entities.Task;
using TodoList = Lab3.services.TodoList;

namespace Lab3;

internal static class Program
{
    private static void Main()
    {
        var todoList = new TodoList();

        var jsonFilePath = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.json";
        var xmlFilePath = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.xml";
        var sqliteFilePath = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.sqlite";

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add task");
            Console.WriteLine("2. Search task");
            Console.WriteLine("3. Last tasks");
            Console.WriteLine("4. Save tasks");
            Console.WriteLine("5. Load tasks");
            Console.WriteLine("6. Exit");

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
                    Console.WriteLine("Choose mode:");
                    Console.WriteLine("1. JSON");
                    Console.WriteLine("2. XML");
                    Console.WriteLine("3. SQLite");
                    Console.WriteLine("4. Exit");

                    var saveChoice = Console.ReadLine();

                    switch (saveChoice)
                    {
                        case "1":
                            todoList.Save(jsonFilePath, models.SaveMode.JSON);
                            break;
                        case "2":
                            todoList.Save(xmlFilePath, models.SaveMode.XML);
                            break;
                        case "3":
                            todoList.Save(sqliteFilePath, models.SaveMode.SQLite);
                            break;
                        case "4":
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                    break;
                case "5":
                    Console.WriteLine("Choose mode:");
                    Console.WriteLine("1. JSON");
                    Console.WriteLine("2. XML");
                    Console.WriteLine("3. SQLite");
                    Console.WriteLine("4. Exit");

                    var loadChoice = Console.ReadLine();

                    switch (loadChoice)
                    {
                        case "1":
                            todoList.Load(jsonFilePath, models.SaveMode.JSON);
                            break;
                        case "2":
                            todoList.Load(xmlFilePath, models.SaveMode.XML);
                            break;
                        case "3":
                            todoList.Load(sqliteFilePath, models.SaveMode.SQLite);
                            break;
                        case "4":
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    } break;
                case "6":
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