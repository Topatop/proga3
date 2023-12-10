using Lab2.exceptions;
using Task = Lab2.entities.Task;

namespace Lab2.services;

public class TodoList
{
    private List<Task> _tasks  = new List<Task>();

    public void Add(Task task)
    {
        if (_tasks.Contains(task)) throw new TaskException("Task already in TodoList");
        
        _tasks.Add(task);
        Console.WriteLine("Task added successfully.");
    }

    public IReadOnlyList<Task> SearchByTag(string tag) => _tasks.Where(task => task.Tags.Contains(tag)).ToList().AsReadOnly();
    public IReadOnlyList<Task> GetCurrent() => _tasks.OrderBy(task => task.Deadline).ToList().AsReadOnly();
}