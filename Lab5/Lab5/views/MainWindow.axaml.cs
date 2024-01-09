using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Lab5.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public static FuncValueConverter<List<string>, string> PrettyPrintTags { get; } =
        new(list => string.Join("; ", list));
}