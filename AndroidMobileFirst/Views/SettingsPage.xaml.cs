using AndroidMobileFirst.Services;

namespace AndroidMobileFirst.Views;

public partial class SettingsPage : ContentPage
{
    private readonly LlmService _ollamaService;

    public SettingsPage()
    {
        InitializeComponent();
        _ollamaService = new LlmService();
    }

    private void OnGiveYourContextClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FileUploadPage());
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        string llmOperatorUrl = LlmOperatorEntry.Text;
        if (!string.IsNullOrEmpty(llmOperatorUrl))
        {
            _ollamaService.SetBaseUrl(llmOperatorUrl);
            DisplayAlert("Success", "LLM operator URL saved successfully.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Please enter a valid URL.", "OK");
        }
    }
}