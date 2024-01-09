using Avalonia;
using Avalonia.ReactiveUI;

namespace Lab5;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args) => AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .WithInterFont()
        .LogToTrace()
        .UseReactiveUI()
        .StartWithClassicDesktopLifetime(args);
}