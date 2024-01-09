using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Lab5.services;
using Lab5.Views;
using ReactiveUI;
using Task = Lab5.entities.Task;

namespace Lab5.ViewModels;

public class MainWindowViewModel
{
    private readonly TodoList _todoList;

    public ObservableCollection<Task> Tasks { get; set; }

    public Task SelectedTask { get; set; }

    public ReactiveCommand<Unit, Unit> EditTaskCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteTaskCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowAddTaskCommand { get; }

    public MainWindowViewModel()
    {
        Tasks = new ObservableCollection<Task>();
        _todoList = new TodoList();

        EditTaskCommand = ReactiveCommand.Create(EditTask);
        DeleteTaskCommand = ReactiveCommand.Create(DeleteSelectedTask);
        ShowAddTaskCommand = ReactiveCommand.Create(ShowAddTaskDialog);
    }

    private void SaveTask(Task task)
    {
        Tasks.Add(task);
        _todoList.Add(task);
    }

    private void ShowAddTaskDialog()
    {
        var addTaskViewModel = new AddTaskViewModel(SaveTask);
        var addTaskView = new AddTaskWindow { DataContext = addTaskViewModel };

        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime
            desktopLifetime) return;

        var mainWindow = desktopLifetime.MainWindow;

        addTaskView.ShowDialog(mainWindow);
    }

    private void EditTask()
    {
        if (SelectedTask == null) return;

        var editTaskViewModel = new AddTaskViewModel(SelectedTask, task =>
        {
            DeleteSelectedTask();
            SaveTask(task);
        });

        var editTaskView = new AddTaskWindow { DataContext = editTaskViewModel };

        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime
            desktopLifetime)
            return;

        var mainWindow = desktopLifetime.MainWindow;
        editTaskView.ShowDialog(mainWindow);
    }

    private void DeleteSelectedTask()
    {
        if (SelectedTask == null) return;
        DeleteTask(SelectedTask);
    }


    private void DeleteTask(Task task)
    {
        _todoList.Remove(task);
        Tasks.Remove(task);
    }
}