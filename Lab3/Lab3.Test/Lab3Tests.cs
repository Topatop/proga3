using Lab3.models;

namespace Lab3.Test;

using System;
using System.Collections.Generic;
using Lab3.entities;
using Lab3.exceptions;
using Lab3.services;

public class Lab3Tests : IDisposable
{
    private const string jsonFileName = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.json";
    private const string xmlFileName = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.xml";
    private const string sqliteFileName = "C:\\Users\\savva\\OneDrive\\Рабочий стол\\учёба\\шарп\\Lab3\\Lab3\\storage\\Data.sqlite";


    [Fact]
    public void AddTask_ShouldAddTaskToList()
    {
        var todoList = new TodoList();
        var task = new Task("Test Theme", "Test Description", DateTime.Now, new List<string> { "TestTag" });

        todoList.Add(task);

        Assert.Contains(task, todoList.GetCurrent());
    }

    [Fact]
    public void AddDuplicateTask_ShouldThrowTaskException()
    {
        var todoList = new TodoList();
        var task = new Task("Test Theme", "Test Description", DateTime.Now, new List<string> { "TestTag" });

        todoList.Add(task);

        Assert.Throws<TaskException>(() => todoList.Add(task));
    }

    [Fact]
    public void SearchByTag_ShouldReturnMatchingTasks()
    {
        var todoList = new TodoList();
        var task1 = new Task("Theme1", "Description1", DateTime.Now, new List<string> { "Tag1" });
        var task2 = new Task("Theme2", "Description2", DateTime.Now, new List<string> { "Tag1", "Tag2" });
        var task3 = new Task("Theme3", "Description3", DateTime.Now, new List<string> { "Tag2" });

        todoList.Add(task1);
        todoList.Add(task2);
        todoList.Add(task3);

        var result = todoList.SearchByTag("Tag1");

        Assert.Contains(task1, result);
        Assert.Contains(task2, result);
        Assert.DoesNotContain(task3, result);
    }


    [Fact]
    public void SaveAndLoadJson_ShouldPreserveTasks()
    {
        var todoList = new TodoList();
        var task1 = new Task("Theme1", "Description1", DateTime.Now, new List<string> { "Tag1" });
        var task2 = new Task("Theme2", "Description2", DateTime.Now, new List<string> { "Tag2" });
        var task3 = new Task("Theme3", "Description3", DateTime.Now, new List<string> { "Tag3" });

        todoList.Add(task1);
        todoList.Add(task2);
        
        todoList.Save(jsonFileName, SaveMode.JSON);
        todoList.Add(task3);
        
        Assert.True(File.Exists(jsonFileName));
        
        todoList.Load(jsonFileName, SaveMode.JSON);
        
        Assert.Equal(2, todoList.GetCurrent().Count);
    }

    [Fact]
    public void SaveAndLoadXml_ShouldPreserveTasks()
    {
        var todoList = new TodoList();
        var task1 = new Task("Theme1", "Description1", DateTime.Now, new List<string> { "Tag1" });
        var task2 = new Task("Theme2", "Description2", DateTime.Now, new List<string> { "Tag2" });
        var task3 = new Task("Theme3", "Description3", DateTime.Now, new List<string> { "Tag3" });

        todoList.Add(task1);
        todoList.Add(task2);
        
        todoList.Save(xmlFileName, SaveMode.XML);
        todoList.Add(task3);
        
        Assert.True(File.Exists(xmlFileName));
        
        todoList.Load(xmlFileName, SaveMode.XML);
        
        Assert.Equal(2, todoList.GetCurrent().Count);
    }

    [Fact]
    public void SaveAndLoadSQLite_ShouldPreserveTasks()
    {
        // Arrange
        var todoList = new TodoList();
        var task1 = new Task("Theme1", "Description1", DateTime.Now, new List<string> { "Tag1" });
        var task2 = new Task("Theme2", "Description2", DateTime.Now, new List<string> { "Tag2" });

        todoList.Add(task1);
        todoList.Add(task2);

        // Act
        todoList.Save(sqliteFileName, SaveMode.SQLite);
        var task3 = new Task("Theme3", "Description3", DateTime.Now, new List<string> { "Tag3" });
        todoList.Add(task3);

        Assert.True(File.Exists(sqliteFileName));
        
        todoList.Load(sqliteFileName, SaveMode.SQLite);
        
        Assert.Equal(2, todoList.GetCurrent().Count);
    }

    public void Dispose()
    {
        // Cleanup: Delete the test file created during the tests
        // if (File.Exists(jsonFileName))
        //     File.Delete(jsonFileName);
        //
        // if (File.Exists(xmlFileName))
        //     File.Delete(xmlFileName);
        //
        // if (File.Exists(sqliteFileName))
        //     File.Delete(sqliteFileName);
    }
}