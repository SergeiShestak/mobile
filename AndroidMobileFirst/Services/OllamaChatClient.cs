using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace AndroidMobileFirst.Services;

public class OllamaChatClient : IChatClient
{
    private readonly OllamaApiClient _ollama;
    public OllamaChatClient(Uri uri, string model)
    {
        _ollama = new OllamaApiClient(uri);
        _ollama.SelectedModel = model;
    }

    public async IAsyncEnumerable<Message> CompleteStreamingAsync(List<Message> chatHistory)
    {
        var request = new ChatRequest
        {
            Model = _ollama.SelectedModel,
            Messages = chatHistory.Select(m => new Message(
                m.Role,
                m.Content
            )).ToList()
        };

        await foreach (var response in _ollama.ChatAsync(request, CancellationToken.None))
        {
            yield return new Message();
        }
    }
}

public class ChatResponse
{
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    public string Text { get; set; }
}