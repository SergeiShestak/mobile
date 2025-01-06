using AndroidMobileFirst.Services;

namespace AndroidMobileFirst.Views;

public partial class SettingsPage : ContentPage
{
    private readonly LlmService _llmService;

    public SettingsPage()
    {
        InitializeComponent();
        _llmService = new LlmService();
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
            DisplayAlert("Success", "LLM operator URL saved successfully.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Please enter a valid URL.", "OK");
        }
    }
}