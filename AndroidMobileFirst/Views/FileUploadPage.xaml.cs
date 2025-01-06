using AndroidMobileFirst.Services;

namespace AndroidMobileFirst.Views;

public partial class FileUploadPage : ContentPage
{
    private readonly LlmService _llmService;
    public FileUploadPage()
    {
        InitializeComponent();
        _llmService = new LlmService();
    }

    private async void OnSelectFileClicked(object sender, EventArgs e)
    {
        var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[]
            { "com.adobe.pdf", "public.wordprocessingml-template"  } },
            { DevicePlatform.Android, new[]
            { "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.template" } },
            { DevicePlatform.WinUI, new[]
            { ".pdf", ".docx"  } }
        });

        var status = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission Denied", "Unable to access storage.", "OK");
            return;
        }

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = customFileType
        });

        if (result == null)
        {
            // No file was picked, navigate back to the previous page
            await DisplayAlert("No File Selected", "No file was selected or the file does not exist.", "OK");
            await Navigation.PopAsync();
            return;
        }

        await SaveFileAsync(result);
    }

    private async Task SaveFileAsync(FileResult file)
    {
        if (file == null)
            return;

        var newFilePath = Path.Combine(FileSystem.AppDataDirectory, file.FileName);

        using (var stream = await file.OpenReadAsync())
        using (var newStream = File.OpenWrite(newFilePath))
        {
            await stream.CopyToAsync(newStream);
        }

        _llmService.ContextUri = new Uri(newFilePath);
        await DisplayAlert("File Saved", $"File saved to {newFilePath}", "OK");
    }
}