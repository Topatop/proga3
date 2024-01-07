namespace Lab4.api;

public class RestApiThread
{
    private readonly Thread _thread;
    private readonly RestApiConfig _config;

    public RestApiThread(RestApiConfig config)
    {
        _thread = new Thread(RunThread);
        _config = config;
    }

    public void Start() => _thread.Start();

    private void RunThread()
    {
        var builder = WebApplication.CreateBuilder();

        builder.WebHost.UseUrls(_config.Url);
        builder.Services.AddSingleton(_config);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Logging.ClearProviders();

        var app = builder.Build();

        app.MapControllers();
        app.Run();
    }
}
