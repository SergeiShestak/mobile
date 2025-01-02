namespace AndroidMobileFirst.Views;
public partial class UserInputPage : ContentPage
{
    public UserInputPage()
    {
        InitializeComponent();
    }

    private async void OnAddRecordClicked(object sender, EventArgs e)
    {
        var date = DatePicker.Date;
        var data = DataEntry.Text;

        if (!string.IsNullOrEmpty(data))
        {
            // Assuming you have a way to access the TablePage instance
            var tablePage = (TablePage)Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is TablePage);
            tablePage?.AddData(date, data);

            await DisplayAlert("Success", "Record added successfully", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Please enter data", "OK");
        }
    }
}