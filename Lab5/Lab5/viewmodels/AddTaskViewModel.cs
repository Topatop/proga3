using System.Reactive;
using ReactiveUI;
using Task = Lab5.entities.Task;

namespace Lab5.ViewModels;

public class AddTaskViewModel
{
    public string Theme { get; set; }

    public string Description { get; set; }

    public string Deadline { get; set; }

    public string Tags { get; set; }

    public ReactiveCommand<Unit, Unit> AddTaskCommand { get; }

    public AddTaskViewModel(Action<Task> onTaskAdded)
    {
        AddTaskCommand = ReactiveCommand.Create(MakeAddTaskCommand(onTaskAdded));
    }

    public AddTaskViewModel(Task existingTask, Action<Task> onTaskAdded)
    {
        Theme = existingTask.Theme;
        Description = existingTask.Description;
        Deadline = existingTask.Deadline.ToString();
        Tags = string.Join(';', existingTask.Tags);

        AddTaskCommand = ReactiveCommand.Create(MakeAddTaskCommand(onTaskAdded));
    }

    private Action MakeAddTaskCommand(Action<Task> @delegate) => () =>
    {
        var tags = Tags.Split(';').ToList();
        var newTask = new Task(Theme, Description, DateTime.Parse(Deadline), tags);
        @delegate.Invoke(newTask);
    };
}