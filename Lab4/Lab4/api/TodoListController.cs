using Lab4.models;
using Lab4.services;
using Microsoft.AspNetCore.Mvc;
using Task = Lab4.entities.Task;

namespace Lab4.api;

[Route("todo")]
[ApiController]
public class TodoListController : ControllerBase
{
    private readonly RestApiConfig _config;
    private readonly TodoList _todoList;

    public TodoListController(RestApiConfig config)
    {
        _config = config;
        _todoList = new TodoList();
        _todoList.Load(_config.JsonPath, SaveMode.JSON);
    }

    [HttpGet]
    public IEnumerable<Task> GetAllTasks()
    {
        return _todoList.Tasks;
    }

    [HttpGet("urgent")]
    public IEnumerable<Task> GetUrgentTasks([FromQuery] int? limit)
    {
        var allUrgentTasks = _todoList.Tasks
            .FindAll(task => task.Deadline >= DateTime.Now)
            .OrderBy(task => task.Deadline);
        return limit == null ? allUrgentTasks : allUrgentTasks.Take(limit.Value);
    }

    [HttpGet("search")]
    public IEnumerable<Task> SearchTasks([FromQuery] string tag)
    {
        return _todoList.SearchByTag(tag);
    }

    [HttpPost]
    public IActionResult AddNewTask(TaskDto newTaskDto)
    {
        _todoList.Add(newTaskDto.toTask());
        _todoList.Save(_config.JsonPath, SaveMode.JSON);
        return Ok();
    }
}