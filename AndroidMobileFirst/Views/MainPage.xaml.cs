namespace AndroidMobileFirst.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		// Example of how to navigate to a new page
		Shell.Current.GoToAsync("///table");
	}
}