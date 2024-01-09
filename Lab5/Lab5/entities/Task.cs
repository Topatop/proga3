namespace Lab5.entities;

public class Task
{
    public string Theme { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<string> Tags { get; set; }

    public Task(string theme, string description, DateTime deadline, List<string> tags)
    {
        Theme = theme;
        Description = description;
        Deadline = deadline;
        Tags = tags;
    }

    public override string ToString()
    {
        return $"Theme: {Theme}\nDescription: {Description}\nDeadline: {Deadline}\nTags: {string.Join(", ", Tags)}\n";
    }
}