using Task = Lab4.entities.Task;

namespace Lab4.api;

public record TaskDto
{
    public string Theme { get; init; }
    public string Description { get; init; }
    public DateTime Deadline { get; init; }
    public List<string>? Tags { get; init; }
}

public static class TaskDtoExtensions
{
    public static Task toTask(this TaskDto dto)
    {
        var tags = dto.Tags ?? new List<string>();
        return new Task(dto.Theme, dto.Description, dto.Deadline, tags);
    }
}
