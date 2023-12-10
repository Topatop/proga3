using System;
using Lab2.exceptions;
using Lab2.services;
using Xunit;
using Task = Lab2.entities.Task;

namespace Lab2.Tests
{
    public class TaskTests
    {
        [Fact]
        public void TaskToString_FormatsCorrectly()
        {
            var task = new Task("Test Theme", "Test Description", DateTime.Now, new List<string> { "Tag1", "Tag2" });

            var result = task.ToString();

            Assert.Contains("Theme: Test Theme", result);
            Assert.Contains("Description: Test Description", result);
            Assert.Contains("Tags: Tag1, Tag2", result);
        }
    }

    public class TodoListTests
    {
        [Fact]
        public void AddTask_SuccessfullyAddsTask()
        {
            var todoList = new TodoList();
            var task = new Task("Test Theme", "Test Description", DateTime.Now, new List<string> { "Tag1", "Tag2" });

            todoList.Add(task);

            Assert.Single(todoList.GetCurrent());
        }

        [Fact]
        public void AddTask_ThrowsExceptionIfTaskAlreadyExists()
        {
            var todoList = new TodoList();
            var task = new Task("Test Theme", "Test Description", DateTime.Now, new List<string> { "Tag1", "Tag2" });

            todoList.Add(task);

            Assert.Throws<TaskException>(() => todoList.Add(task));
        }

        [Fact]
        public void SearchByTag_ReturnsMatchingTasks()
        {
            var todoList = new TodoList();
            var task1 = new Task("Test Theme 1", "Test Description", DateTime.Now, new List<string> { "Tag1", "Tag2" });
            var task2 = new Task("Test Theme 2", "Test Description", DateTime.Now, new List<string> { "Tag2", "Tag3" });

            todoList.Add(task1);
            todoList.Add(task2);

            var result = todoList.SearchByTag("Tag2");

            Assert.Equal(2, result.Count);
        }
    }
}