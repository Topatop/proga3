using System.Xml.Serialization;
using Lab3.exceptions;
using Lab3.models;
using Lab3.repositories;
using Task = Lab3.entities.Task;

namespace Lab3.services;

[Serializable]
public class TodoList
{
    private List<Task> _tasks  = new List<Task>();
    private SQLiteTaskRepository<TodoList> _sqLiteRepository;
    
    public TodoList() {}
    
    public List<Task> Tasks
    {
        get => _tasks;
        set => _tasks = value;
    }

    public void Add(Task task)
    {
        if (_tasks.Contains(task)) throw new TaskException("Task already in TodoList");
        
        _tasks.Add(task);
        Console.WriteLine("Task added successfully.");
    }

    public IReadOnlyList<Task> SearchByTag(string tag) => _tasks.Where(task => task.Tags.Contains(tag)).ToList().AsReadOnly();
    public IReadOnlyList<Task> GetCurrent() => _tasks.OrderBy(task => task.Deadline).ToList().AsReadOnly();
    
    public void Save(string fileName, SaveMode saveMode)
    {
        switch (saveMode)
        {
            case SaveMode.JSON:
                SaveToJson(fileName);
                break;
            case SaveMode.XML:
                SaveToXml(fileName);
                break;
            case SaveMode.SQLite:
                SaveToSQLite(fileName);
                break;
            default:
                throw new ArgumentException("Invalid save mode");
        }
    }
    
    public void Load(string fileName, SaveMode saveMode)
    {
        switch (saveMode)
        {
            case SaveMode.JSON:
                LoadFromJson(fileName);
                break;
            case SaveMode.XML:
                LoadFromXml(fileName);
                break;
            case SaveMode.SQLite:
                LoadFromSQLite(fileName);
                break;
            default:
                throw new ArgumentException("Invalid save mode");
        }
    }
    
    private void SaveToJson(string filePath)
    {
        var jsonRepository = new JsonRepository<TodoList>();
        jsonRepository.Save(this, filePath);
        Console.WriteLine("Data saved to JSON successfully.");
    }

    private void LoadFromJson(string fileName)
    {
        var jsonRepository = new JsonRepository<TodoList>();
        var loadedTodoList = jsonRepository.Load(fileName);
        _tasks = loadedTodoList.Tasks;
    }

    private void SaveToXml(string fileName)
    {
        var xmlRepository = new XmlRepository<TodoList>();
        xmlRepository.Save(this, fileName);
    }

    private void LoadFromXml(string fileName)
    {
        var xmlRepository = new XmlRepository<TodoList>();
        var loadedTodoList = xmlRepository.Load(fileName);
        _tasks = loadedTodoList.Tasks;
    }

    private void SaveToSQLite(string databasePath)
    {
        _sqLiteRepository = new SQLiteTaskRepository<TodoList>();
        _sqLiteRepository.Save(this, databasePath);
    }

    private void LoadFromSQLite(string databasePath)
    {
        if (_sqLiteRepository == null) throw new Exception("No saves were made so far.");
        var loadedTodoList = _sqLiteRepository.Load(databasePath);
        _tasks = loadedTodoList.Tasks;
    }
}