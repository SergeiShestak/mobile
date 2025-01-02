using System;
using Microsoft.Maui.Controls;

namespace AndroidMobileFirst.Views;
public partial class TablePage : ContentPage
{
    public TablePage()
    {
        InitializeComponent();
        PopulateTable();
    }

    private void PopulateTable()
    {
        // Example data for the table
        var data = new[]
        {
                new { Name = "Item 1", Description = "Description 1" },
                new { Name = "Item 2", Description = "Description 2" },
                new { Name = "Item 3", Description = "Description 3" }
            };

        foreach (var item in data)
        {
            var nameLabel = new Label { Text = item.Name };
            var descriptionLabel = new Label { Text = item.Description };

            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { nameLabel, descriptionLabel }
            };

            // Assuming there's a StackLayout named TableStackLayout in TablePage.xaml
            //TableStackLayout.Children.Add(stackLayout);
        }
    }
}