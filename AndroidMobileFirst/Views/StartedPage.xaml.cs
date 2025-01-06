using AndroidMobileFirst.Services;

namespace AndroidMobileFirst.Views;

public partial class StartedPage : ContentPage
{
    private readonly LlmService _llmService;

    public StartedPage()
    {
        InitializeComponent();
        _llmService = new LlmService();
        DisplayResponse();
    }

    private async void DisplayResponse()
    {
            string response = await _llmService.GetResponseAsync("Hello from MAUI lets get started!");
            ResponseLabel.Text = response;
    }

    private void OnSubmitResponseClicked(object sender, EventArgs e)
    {
        _llmService.IsSummaryTrue = Option1.IsChecked ? "Agree" : Option2.IsChecked ? "Disagree" : string.Empty;
        bool isCorrect = ResponseComparer.CompareResponse(_llmService.IsSummaryTrue, "Expected Answer");
        DisplayAlert("Response", isCorrect ? "Correct!" : "Incorrect!", "OK");
    }
}