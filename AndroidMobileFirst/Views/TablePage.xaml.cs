namespace AndroidMobileFirst.Views;

public partial class TablePage : ContentPage
{
    private Dictionary<DateTime, string>? _internalStorage;
    private StackLayout TableStackLayout;

    public TablePage()
    {
        InitializeComponent();
        TableStackLayout = new StackLayout();
        PopulateTable();
    }

    private void PopulateTable()
    {
        // Clear existing children
        TableStackLayout.Children.Clear();

        // Populate the table with data from _internalStorage
        foreach (var entry in _internalStorage)
        {
            TableStackLayout.Children.Add(new Label { Text = $"{entry.Key}: {entry.Value}" });
        }
    }

    public void AddData(DateTime date, string data)
    {
        _internalStorage[date] = data;
        PopulateTable(); // Refresh the table view
    }

    public string GetData(DateTime date)
    {
        return _internalStorage.TryGetValue(date, out var data) ? data : null;
    }

    private async void OnAddRecordButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserInputPage());
    }
}