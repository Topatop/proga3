namespace Lab4.api;

public record RestApiConfig
{
    public string Url { get; init; }
    public string JsonPath { get; init; }
}
