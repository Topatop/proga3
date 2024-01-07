namespace Lab4.entities;

[Serializable]
public class Task
{
    public String Theme { get; set; }
    public String Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<String> Tags { get; set; }
    
    public Task() {}
    
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