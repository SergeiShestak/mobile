using AndroidMobileFirst.Models;
using OllamaSharp.Models.Chat;

namespace AndroidMobileFirst.Services
{
    public interface IChatClient
    {
           IAsyncEnumerable<Message> CompleteStreamingAsync(List<Message> chatHistory);
    }
}