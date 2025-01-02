using Microsoft.Maui.Controls;

namespace AndroidMobileFirst.Views;

public class TableStackLayout : StackLayout
{
    public TableStackLayout()
    {
        Orientation = StackOrientation.Vertical;
    }

    public void AddRecord(DateTime date, string data)
    {
        var dateLabel = new Label { Text = date.ToString("d") };
        var dataLabel = new Label { Text = data };

        var stackLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children = { dateLabel, dataLabel }
        };

        Children.Add(stackLayout);
    }

    public void ClearRecords()
    {
        Children.Clear();
    }
}