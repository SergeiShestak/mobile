using AndroidMobileFirst.Services;

namespace AndroidMobileFirst.Views;

public partial class StartedPage : ContentPage
{
    private readonly LlmService _ollamaService;

    public StartedPage()
    {
        InitializeComponent();
        _ollamaService = new LlmService();
        DisplayResponse();
    }

    private async void OnTableClicked(object sender, EventArgs e)
    {
        // Navigate to the FileUploadPage when the button is clicked
        await Navigation.PushAsync(new FileUploadPage());
    }

    private async void DisplayResponse()
    {
        try
        {
            string response = await _ollamaService.GetResponseAsync("Your query here");
            ResponseLabel.Text = response;
        }
        catch (Exception ex)
        {
            ResponseLabel.Text = $"Error: {ex.Message}";
        }
    }

    private void OnSubmitResponseClicked(object sender, EventArgs e)
    {
        string userResponse = Option1.IsChecked ? "Option 1" : Option2.IsChecked ? "Option 2" : string.Empty;
        bool isCorrect = ResponseComparer.CompareResponse(userResponse, "Expected Answer");
        DisplayAlert("Response", isCorrect ? "Correct!" : "Incorrect!", "OK");
    }
}