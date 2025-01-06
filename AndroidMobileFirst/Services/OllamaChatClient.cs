using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AndroidMobileFirst.Services;

public enum ChatRole
{
    User,
    Assistant
}

public class ChatMessage
{
    public ChatRole Role { get; set; }
    public string Text { get; set; }

    public ChatMessage(ChatRole role, string text)
    {
        Role = role;
        Text = text;
    }
}

public interface IChatClient
{
    IAsyncEnumerator<ChatMessage> CompleteStreamingAsync(List<ChatMessage> chatHistory);
}

public class OllamaChatClient : IChatClient
{
    private readonly Uri _ollamaUri;
    private readonly string _model;
    private readonly HttpClient _httpClient;

    public OllamaChatClient(Uri ollamaUri, string model)
    {
        _ollamaUri = ollamaUri;
        _model = model;
        _httpClient = new HttpClient();
    }

    public async IAsyncEnumerator<ChatMessage> CompleteStreamingAsync(List<ChatMessage> chatHistory)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _ollamaUri);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your_token");

        var payload = new
        {
            model = _model,
            prompt = string.Join("\n", chatHistory.Select(m => m.Text)),
            max_tokens = 2048,
            temperature = 0.7
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ChatResponse>(responseBody);

        foreach (var message in result.choices)
        {
            yield return new ChatMessage(ChatRole.Assistant, message.text);
        }
    }
}

public class ChatResponse
{
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public string text { get; set; }
}